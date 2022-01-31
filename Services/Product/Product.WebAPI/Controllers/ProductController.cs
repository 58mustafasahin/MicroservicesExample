using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Product.Business.Abstract;
using Product.DAL.Dto.Category;
using Product.DAL.Dto.Product;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Product.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                Request.Headers.TryGetValue("Authorization", out var token);

                var product = await _productService.GetById(id);

                Uri myUri = new Uri($"https://localhost:44388/category/{product.CategoryId}");
                WebRequest webRequest = WebRequest.Create(myUri);
                var aa = (HttpWebRequest)webRequest;
                aa.UseDefaultCredentials = true;
                aa.PreAuthenticate = true;
                aa.Headers.Add("Authorization", token);
                WebResponse webResponse = await webRequest.GetResponseAsync();
                using (Stream dataStream = webResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);
                    string res = await reader.ReadToEndAsync();
                    var json = JsonConvert.DeserializeObject<CategoryDto>(res);
                    product.CategoryName = json.Name;
                }
                if (product == null) return Ok();
                return Ok(product);
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
                Request.Headers.TryGetValue("Authorization", out var token);

                var productList = await _productService.GetList();
                foreach (var product in productList)
                {
                    Uri myUri = new Uri($"https://localhost:44388/category/{product.CategoryId}");
                    WebRequest webRequest = WebRequest.Create(myUri);
                    var aa = (HttpWebRequest)webRequest;
                    aa.UseDefaultCredentials = true;
                    aa.PreAuthenticate = true;
                    aa.Headers.Add("Authorization", token);
                    WebResponse webResponse = await webRequest.GetResponseAsync();
                    using (Stream dataStream = webResponse.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(dataStream);
                        string res = await reader.ReadToEndAsync();
                        var json = JsonConvert.DeserializeObject<CategoryDto>(res);
                        product.CategoryName = json.Name;
                    }
                }
                return Ok(productList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductDto addProduct)
        {
            try
            {
                var result = await _productService.Upsert(addProduct);
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
        public async Task<IActionResult> Update(ProductDto updateProduct)
        {
            try
            {
                var result = await _productService.Upsert(updateProduct);
                var list = new Lazy<List<string>>();
                switch (result)
                {
                    case > 0:
                        list.Value.Add("Güncelleme işlemi başarılı.");
                        return Ok(new { code = StatusCode(1000), message = list.Value, type = "success" });
                    case -1:
                        list.Value.Add("Ürün bulunamadı.");
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
                var result = await _productService.Delete(id);
                var list = new Lazy<List<string>>();
                switch (result)
                {
                    case > 0:
                        list.Value.Add("Silme işlemi başarılı.");
                        return Ok(new { code = StatusCode(1000), message = list.Value, type = "success" });
                    case -1:
                        list.Value.Add("Ürün bulunamadı.");
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
