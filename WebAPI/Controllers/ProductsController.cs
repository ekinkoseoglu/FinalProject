﻿using System.Threading;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("getall")]
      //  [Authorize(Roles = "product.list")] // Bu operasyonu çalıştırabilmek için kişinin Authantice olması yeterli yani buna istek yaptığında elinde bir token'i olması yeterli (Yani sisteme giriş yapmış olması yeterli)
        public IActionResult Get()
        {
            var result = _productService.GetAll();
            Thread.Sleep(1000); // For Bootstrap spinner
            if (result.Success)
            {
                return Ok(result); // Statu code 200
            }

            return BadRequest(result); // Statu code 400


        }

        [HttpPost("add")]
        public IActionResult Post(Product product)
        {
            var result = _productService.Add(product);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult Get(int id)
        {
            var result = _productService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbycategory")]
        public IActionResult GetByCategoryId(int categoryId)
        {
            var result = _productService.GetAllByCategoryId(categoryId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
