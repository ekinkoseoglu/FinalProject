using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private ICategoryService _categoryService;

        public CategoriesController(ICategoryService Categorieservice)
        {
            _categoryService = Categorieservice;
        }

        [HttpGet("getall")]
        //  [Authorize(Roles = "Category.list")] // Bu operasyonu çalıştırabilmek için kişinin Authantice olması yeterli yani buna istek yaptığında elinde bir token'i olması yeterli (Yani sisteme giriş yapmış olması yeterli)
        public IActionResult Get()
        {
            var result = _categoryService.GetAll();

            if (result.Success)
            {
                return Ok(result); // Statu code 200
            }

            return BadRequest(result); // Statu code 400


        }

        [HttpPost("add")]
        public IActionResult Post(Category Category)
        {
            var result = _categoryService.Add(Category);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult Get(int id)
        {
            var result = _categoryService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
