using RestAPI.Common;
using RestAPI.DAL;
using RestAPI.Model;

namespace RestAPI.Business_Layer
{
    public class BLLAdmin
    {
        public List<UserEntity> userdetails;
        public BLLAdmin()
        {
            userdetails = new List<UserEntity>()
            {
                new UserEntity()
                {
                    Id = 1,
                    Name="Arka",
                    Email="arka@gmail.com",
                    Gender="Male"
                },
                new UserEntity()
                {
                    Id = 2,
                    Name="Oindrila",
                    Email="oindrila@gmail.com",
                    Gender="Female"
                },
                new UserEntity()
                {
                    Id = 3,
                    Name="kankana",
                    Email="kanakana@gmail.com",
                    Gender="Female"
                }
            };
        }
        public BllOutput getAdminData()
        {
            try
            {
                //userdetails will be replaced by db :1000000
                return new BllOutput()
                {
                    Data = userdetails,
                    IsSuccess = true
                };
            }
            catch(Exception ex)
            {

            }
            return null;
        }

        public BllOutput getAdminDataFilteredByGender(string AdminGender)
        {
            try
            {
                var FilteredAdminbyGender = userdetails.Where(x => x.Gender == AdminGender).ToList();
                return new BllOutput()
                {
                    Data = FilteredAdminbyGender,
                    IsSuccess = true
                };
            }
            catch (Exception ex) 
            {
            }
            return null;
        }

        public BllOutput getDepartmentData()
        {
            try
            {
                using (var DB = new FacultyMgmtSysContext())
                {
                    var res = DB.TDeptMsts.Select(x => new
                    {
                        DeptId = x.DeptId,
                        DeptName = x.DeptName,
                        Status = x.IsActive ? "Active" : "Inactive"
                    }).ToList();
                    return new BllOutput()
                    {
                        Data = res,
                        IsSuccess = true
                    };
                }
                   
            }
            catch (Exception ex)
            {

            }
            return null;
        }

    }
}
