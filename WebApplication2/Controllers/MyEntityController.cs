using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MyEntityController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly ApplicationDbContext _context;

        public MyEntityController(IWebHostEnvironment env, ApplicationDbContext context)
        {
            _env = env;
            _context = context;
        }

        [HttpPost]
         public async Task<IActionResult> Create(MyDto dto)
         {
             if (!ModelState.IsValid)
             {
                 return BadRequest(ModelState);
             }

             // Subir imagen a la carpeta wwwroot/upload
             string uploadsFolder = Path.Combine(_env.WebRootPath, "upload");
             string uniqueFileName = Guid.NewGuid().ToString() + "_" + dto.Image.FileName;
             string filePath = Path.Combine(uploadsFolder, uniqueFileName);
             using (var stream = new FileStream(filePath, FileMode.Create))
             {
                 await dto.Image.CopyToAsync(stream);
             }

             // Crear entidad y guardar en la base de datos
             MyEntity entity = new MyEntity
             {
                 Detecto = dto.Detecto,
                 ImagePath = uniqueFileName
             };
             _context.MyEntities.Add(entity);
             await _context.SaveChangesAsync();

             //return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);

             return Ok(entity);
         }
    }
}
