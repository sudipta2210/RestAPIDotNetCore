using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RestAPI.Business_Layer;
using RestAPI.Common;
using RestAPI.Model;
using System.Security.Cryptography;

namespace RestAPI.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class AdminController : ControllerBase
    {
        public BLLAdmin Admin;

        #region caching
        // Custom cache manager to handle caching.
        private readonly MemoryCaching  _cache;
        // Configuration for reading settings from appsettings.json.
        private readonly IConfiguration _configuration;
        // Cache expiration durations.
        private readonly int _CacheAbsoluteDurationMinutes;
        private readonly int _CacheSlidingDurationMinutes;
        private readonly MemoryCacheEntryOptions _cacheEntryOptions;
        #endregion

        public AdminController(MemoryCaching cache, IConfiguration configuration)
        {
            Admin = new BLLAdmin();
            _cache = cache;
            _configuration = configuration;
            _CacheAbsoluteDurationMinutes = _configuration.GetValue<int?>("CacheSettings:CacheAbsoluteDurationMinutes") ?? 30;
            _CacheSlidingDurationMinutes = _configuration.GetValue<int?>("CacheSettings:CacheSlidingDurationMinutes") ?? 30;
            _cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetPriority(CacheItemPriority.High); // Countries are considered critical data.
        }

        [Authorize]
        [HttpGet]
        public CommonEntity GetAdmindetails()
        {
            //BLLAdmin Admin=new BLLAdmin();
            try
            {
                var CACHEKEY = "Admin" + "_" + "API";

                if (_cache.IsExist<object>(CACHEKEY, out var cache)) //check whether any data exists in memory or not against the cachekey 
                {
                    var data = _cache.Get<object>(CACHEKEY);
                    return new CommonEntity
                    {
                        Data = data,
                        IsSuccess = true,
                        Message = "User has been Fetched from Cache",
                        StatusCode = 200
                    };
                }

                var userData = Admin.getAdminData();
                if (userData.IsSuccess)
                {
                    _cache.Set<object>(CACHEKEY, userData.Data, _cacheEntryOptions);
                    return new CommonEntity
                    {
                        Data = userData.Data,
                        IsSuccess = true,
                        Message = "User has been Fetched",
                        StatusCode = 200
                    };
                }
                else
                {
                    return new CommonEntity
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = "Internal Error!!",
                        StatusCode = 200
                    };
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        [Authorize]
        [HttpPost]
        public CommonEntity GetAdmindetailsByGender(string Gender)
        {
            //BLLAdmin Admin = new BLLAdmin();
            try
            {
                var userData = Admin.getAdminDataFilteredByGender(Gender);
                if (userData.IsSuccess)
                {
                    return new CommonEntity
                    {
                        Data = userData.Data,
                        IsSuccess = true,
                        Message = "User has been Fetched",
                        StatusCode = 200
                    };
                }
                else
                {
                    return new CommonEntity
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = "Internal Error!!",
                        StatusCode = 200
                    };
                }
            }
            catch (Exception ex) { }
            return null;

        }

        [Authorize]
        [HttpPost]
        public CommonEntity InsertUser(UserEntity Input)
        {
            return new CommonEntity { Data = null, IsSuccess = true, Message = "User has been Inserted", StatusCode = 200 };
        }

        [Authorize]
        [HttpGet]
        public CommonEntity GetDepartmentDetails()
        {
            try
            {
                var CACHEKEY = "Department" + "_" + "API";
                if (_cache.IsExist<object>(CACHEKEY, out var cache)) //check whether any data exists in memory or not against the cachekey 
                {
                    var data = _cache.Get<object>(CACHEKEY);
                    return new CommonEntity
                    {
                        Data = data,
                        IsSuccess = true,
                        Message = "Department has been Fetched from Cache",
                        StatusCode = 200
                    };
                }
                var userData = Admin.getDepartmentData();
                if (userData.IsSuccess)
                {
                    _cache.Clear(CACHEKEY);
                    _cache.Set<object>(CACHEKEY, userData.Data, _cacheEntryOptions);
                    return new CommonEntity
                    {
                        Data = userData.Data,
                        IsSuccess = true,
                        Message = "Department has been Fetched",
                        StatusCode = 200
                    };
                }
                else
                {
                    return new CommonEntity
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = "Internal Error!!",
                        StatusCode = 200
                    };
                }
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        [Authorize]
        [HttpPost]
        public CommonEntity InsertNewPaper(string PaperName)
        {
            try
            {
                var BLLCall = Admin.InsertNewPaper(PaperName);
                if (BLLCall.IsSuccess)
                {
                    return new CommonEntity { Data = null, IsSuccess = true, Message = "Paper has been Inserted", StatusCode = 200 };
                }
                else
                {
                    return new CommonEntity { Data = null, IsSuccess = false, Message = "Internal Error!!!", StatusCode = 200 };
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        [Authorize]
        [HttpPost]
        public CommonEntity InsertNewTeacher(string TeacherName)
        {
            try
            {
                var BLLCall = Admin.InsertNewTeacher(TeacherName);
                if (BLLCall.IsSuccess)
                {
                    return new CommonEntity { Data = null, IsSuccess = true, Message = "Teacher has been Inserted", StatusCode = 200 };
                }
                else
                {
                    return new CommonEntity { Data = null, IsSuccess = false, Message = "Internal Error!!!", StatusCode = 200 };
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }


        [HttpPost]
        public CommonEntity signup(SignUpEntity Input)
        {
            try
            {
                var pwd = Input.Password;
                using (Aes myAes = Aes.Create())
                {
                    byte[] encrypted = EncryptDecrypt.Encrypt(pwd, myAes.Key, myAes.IV);

                    string encryptedBase64 = Convert.ToBase64String(encrypted);

                    Input.EncryptedPwd = encryptedBase64;
                    var BLLCall = Admin.SignUp(Input);
                    if (BLLCall.IsSuccess)
                    {
                        var JWTOKEN=Auth.GenerateToken(Input, _configuration);

                        //ADD JWT TOKEN TO THE COOKIES//
                        var cookieoptions = new CookieOptions
                        {
                            HttpOnly=true,
                            Secure=true,
                            SameSite=SameSiteMode.Strict,
                            Expires=DateTime.UtcNow.AddMinutes(30)
                        };
                        Response.Cookies.Append("X-Access-Token", JWTOKEN, cookieoptions);
                        //ENDS HERE
                        return new CommonEntity { Data =null , IsSuccess = true, Message = "Sign Up Successfull", StatusCode = 200 };
                    }
                    else
                    {
                        return new CommonEntity { Data = null, IsSuccess = false, Message = "Internal Error!!!", StatusCode = 200 };
                    }
                }
                return new CommonEntity { Data = null, IsSuccess = true, Message = "Sign up successfull", StatusCode = 200 };
            }
            catch (Exception ex)
            {
            }
            return null;
        }
    }
}
