using ApiECommerce.Context;
using ApiECommerce.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiECommerce.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuariosController : ControllerBase
{
    private readonly AppDbContext dbContext;
    private readonly IConfiguration _config;

    public UsuariosController(AppDbContext dbContext, IConfiguration config)
    {
        _config = config;
        this.dbContext = dbContext;
    }

    [HttpPost("[action]")]
    [ProducesResponseType(StatusCodes.Status201Created)] 
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] Usuario usuario)
    {
        var usuarioExiste = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.Email == usuario.Email);

        if (usuarioExiste is not null)
        {
            return BadRequest("Já existe usuário com este email");
        }

        dbContext.Usuarios.Add(usuario);
        await dbContext.SaveChangesAsync();
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Login([FromBody] Usuario usuario)
    {
        var usuarioAtual = await dbContext.Usuarios.FirstOrDefaultAsync(u =>
                                 u.Email == usuario.Email && u.Senha == usuario.Senha);

        if (usuarioAtual is null)
        {
            return NotFound("Usuário não encontrado");
        }

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Email , usuario.Email)
        };

        var token = new JwtSecurityToken(
            issuer: _config["JWT:Issuer"],
            audience: _config["JWT:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(30),
            signingCredentials: credentials);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return new ObjectResult(new
        {
            access_token = jwt,
            token_type = "bearer",
            user_id = usuarioAtual.Id,
            user_name = usuarioAtual.Nome
        });
    }

    [Authorize]
    [HttpPost("uploadfoto")]
    public async Task<IActionResult> UploadFotoUsuario(IFormFile imagem)
    {
        var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        var usuario = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.Email == userEmail);

        if (usuario is null)
        {
            return NotFound("Usuário não encontrado");
        }

        if (imagem is not null)
        {
            // Gera um nome de arquivo unico para a imagem enviada 
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + imagem.FileName;
            string filePath = Path.Combine("wwwroot/userimages", uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imagem.CopyToAsync(stream);
            }

            // Atualiza a propriedade UrlImagem do usuário com a URL da imagem enviada
            // Assume que a raiz do projeto web é o root
            usuario.UrlImagem = "/userimages/" + uniqueFileName; 

            await dbContext.SaveChangesAsync();
            return Ok("Imagem enviada com sucesso");
        }

        return BadRequest("Nenhuma imagem enviada");
    }

    [Authorize]
    [HttpGet("imagemperfil")]
    public async Task<IActionResult> ImagemPerfilUsuario()
    {
        //verifica se o usuário esta autenticado
        var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

        var usuario = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.Email == userEmail);

        if (usuario is null) 
          return NotFound("Usuário não encontrado");

        var imagemPerfil = await dbContext.Usuarios
            .Where(x => x.Email == userEmail)
            .Select(x => new
            {
                x.UrlImagem,
            })
            .SingleOrDefaultAsync();

        return Ok(imagemPerfil);
    }
}
