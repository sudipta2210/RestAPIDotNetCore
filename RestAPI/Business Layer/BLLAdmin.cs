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
            return new BllOutput()
            {
                Data = null,
                IsSuccess = false
            };
        }

        public BllOutput InsertNewPaper(string PaperName)
        {
            try
            {
                using (var DB=new FacultyMgmtSysContext())
                {
                    var newPaper = new TPaperMst()
                    {
                        PaperName = PaperName,
                        IsActive=true
                    };
                    DB.TPaperMsts.Add(newPaper);

                    var NewSem = new TSemMst()
                    {
                        SemesterNumber = 1,
                        IsOdd = true,
                        IsActive = true
                    };
                    DB.TSemMsts.Add(NewSem);

                    DB.SaveChanges();

                    return new BllOutput()
                    {
                        Data = true,
                        IsSuccess = true
                    };
                }
            }
            catch(Exception ex)
            {

            }
            return new BllOutput()
            {
                Data = null,
                IsSuccess = false
            };
        }


        public BllOutput InsertNewTeacher(string TeacherName)
        {
            try
            {
                using (var DB = new FacultyMgmtSysContext())
                {
                    //var newsampledept = new TDeptMst()
                    //{
                    //    DeptName = "Sample Dept1",
                    //    IsActive = true
                    //};
                    //DB.TDeptMsts.Add(newsampledept);
                    //DB.SaveChanges(); //dot net server  ==> sql server

                    ////var LatestDeptId=DB.TDeptMsts.ToList()
                    ////    .OrderByDescending(x=>x.DeptId).
                    ////    Select(y=>y.DeptId)
                    ////    .FirstOrDefault();

                    //var newTeacher = new TTeacherMst()
                    //{
                    //    TeacherName = TeacherName,
                    //    DeptId = newsampledept.DeptId,
                    //    IsCommon = false,
                    //    CreatedOn = DateTime.Now,
                    //    IsActive = true
                    //};


                    //DB.TTeacherMsts.Add(newTeacher);

                    //DB.SaveChanges();


                    var newsampledept = new TDeptMst()
                    {
                        DeptName = "Sample Dept1",
                        IsActive = true
                    };

                    var newTeacher = new TTeacherMst()
                    {
                        TeacherName = TeacherName,
                        Dept=newsampledept,
                        IsCommon = false,
                        CreatedOn = DateTime.Now,
                        IsActive = true
                    };


                    DB.TTeacherMsts.Add(newTeacher);

                    DB.SaveChanges();

                    return new BllOutput()
                    {
                        Data = true,
                        IsSuccess = true
                    };
                }
            }
            catch (Exception ex)
            {

            }
            return new BllOutput()
            {
                Data = null,
                IsSuccess = false
            };
        }

    }
}
