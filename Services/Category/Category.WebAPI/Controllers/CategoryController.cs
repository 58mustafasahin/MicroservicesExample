using Category.Business.Abstract;
using Category.DAL.Dto.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Category.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var category = await _categoryService.GetById(id);
                if (category == null) return Ok();
                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var categoryList = await _categoryService.GetList();
                return Ok(categoryList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryDto addCategory)
        {
            try
            {
                var result = await _categoryService.Upsert(addCategory);
                var list = new Lazy<List<string>>();
                switch (result)
                {
                    case > 0:
                        list.Value.Add("Ekleme işlemi başarılı.");
                        return Ok(new { code = StatusCode(1000), message = list.Value, type = "success" });
                    case 0:
                        list.Value.Add("Ekleme işlemi başarısız.");
                        return Ok(new { code = StatusCode(1001), message = list.Value, type = "error" });
                    default:
                        list.Value.Add("İşlem sırasında hata meydana geldi.");
                        return Ok(new { code = StatusCode(1001), message = list.Value, type = "error" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(CategoryDto updateCategory)
        {
            try
            {
                var result = await _categoryService.Upsert(updateCategory);
                var list = new Lazy<List<string>>();
                switch (result)
                {
                    case > 0:
                        list.Value.Add("Güncelleme işlemi başarılı.");
                        return Ok(new { code = StatusCode(1000), message = list.Value, type = "success" });
                    case -1:
                        list.Value.Add("Kategori bulunamadı.");
                        return Ok(new { code = StatusCode(1001), message = list.Value, type = "error" });
                    case 0:
                        list.Value.Add("Güncelleme işlemi başarısız.");
                        return Ok(new { code = StatusCode(1001), message = list.Value, type = "error" });
                    default:
                        list.Value.Add("İşlem sırasında hata meydana geldi.");
                        return Ok(new { code = StatusCode(1001), message = list.Value, type = "error" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _categoryService.Delete(id);
                var list = new Lazy<List<string>>();
                switch (result)
                {
                    case > 0:
                        list.Value.Add("Silme işlemi başarılı.");
                        return Ok(new { code = StatusCode(1000), message = list.Value, type = "success" });
                    case -1:
                        list.Value.Add("Kategori bulunamadı.");
                        return Ok(new { code = StatusCode(1001), message = list.Value, type = "error" });
                    case 0:
                        list.Value.Add("Silme işlemi başarısız.");
                        return Ok(new { code = StatusCode(1001), message = list.Value, type = "error" });
                    default:
                        list.Value.Add("İşlem sırasında hata meydana geldi.");
                        return Ok(new { code = StatusCode(1001), message = list.Value, type = "error" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
