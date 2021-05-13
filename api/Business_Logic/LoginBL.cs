using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Data;

namespace api.Business_Logic
{
    //Appusers appUserObj = new Appusers();
    public class LoginBL
    {
        public DataContext _context;

        public ResultReturn<AppUser> GetLogindetails(AppUser appuser)
        {
        ResultReturn<AppUser>  ResultReturn = new ResultReturn<AppUser> ();

            AppUser User = _context.UsersDetail.Where(y => y.Username == appuser.Username && y.Password == appuser.Password).Select(i=>new AppUser(){

Username=i.Username,
Password=i.Password,
UserId=i.UserId,
Role=i.Role

            }).FirstOrDefault();
            if (User == null)
            {
                ResultReturn.Status_Code = Enums.ResultStatus.InvalidLogin;
                ResultReturn.Exception=Enum.GetName(typeof(Enums.ResultStatus),Enums.ResultStatus.InvalidLogin);
            }
            else
            {
                ResultReturn.Id = User.UserId;
                ResultReturn.Status_Code = Enums.ResultStatus.Success;
ResultReturn.Data=User;
            }

            return ResultReturn;

        }
 public ResultReturn<List<AppUserDetail>> GetUser(AppUserDetail appuser,int numberOfObjectsPerPage=25,int pageNumber=1)
        {
 ResultReturn<List<AppUserDetail>>  ResultReturn = new ResultReturn<List<AppUserDetail>> ();
 var ListData=_context.UsersDetail.Where(o=>(o.UserId==appuser.UserId || appuser.UserId==0) 
            && (o.Username==appuser.Username || String.IsNullOrEmpty(appuser.Username)) ).ToList() ;
            ResultReturn.Data  = ListData
            .Skip(numberOfObjectsPerPage * pageNumber)
  .Take(numberOfObjectsPerPage).ToList();
           ;
           ResultReturn.Id=ListData.Count();
            

            return ResultReturn;

        }
        public ResultReturn<int> DeleteUser(AppUserDetail appuser)
        {
 ResultReturn<int>  ResultReturn = new ResultReturn<int> ();
        ;
        _context.UsersDetail.Remove(_context.UsersDetail.Where(o=>(o.UserId==appuser.UserId)).FirstOrDefault());
           ;
            _context.SaveChanges();
ResultReturn.Data=1;
            return ResultReturn;

        }
        public ResultReturn<AppUserDetail> SaveUserDetail(AppUserDetail appUserDetail)
        {
            ResultReturn<AppUserDetail>  ResultReturn = new ResultReturn<AppUserDetail> ();
            using (_context)
            {
                var UserExist = _context.UsersDetail.Where(y => y.UserId == appUserDetail.UserId ).Any();
                if (UserExist)
                {
                    var userDetail = _context.UsersDetail.Where(y => y.UserId == appUserDetail.UserId).FirstOrDefault();
                    AppUserDetailHistory his = new AppUserDetailHistory()
                    {

                        UserId = userDetail.UserId,
                        Address = userDetail.Address,
                        Biodata = userDetail.Biodata,
                        Birthplace = userDetail.Birthplace,
                        Category = userDetail.Category,
                        Contactno = userDetail.Contactno,
                        Designation = userDetail.Designation,
                        Dob = userDetail.Dob,
                        Email = userDetail.Email,
                        Image = userDetail.Image,
                        Name = userDetail.Name,
                        Publications = userDetail.Publications,
                        status = userDetail.status,
                        createdate = userDetail.createdate,
                        createdby = userDetail.createdby
                    };
                    _context.UsersDetailHistory.Add(his);
                    userDetail.Address = appUserDetail.Address;
                    userDetail.Biodata = appUserDetail.Biodata;
                    userDetail.Birthplace = appUserDetail.Birthplace;
                    userDetail.Category = appUserDetail.Category;
                    userDetail.Contactno = appUserDetail.Contactno;
                    userDetail.Designation = appUserDetail.Designation;
                    userDetail.Dob = appUserDetail.Dob;
                    userDetail.Email = appUserDetail.Email;
                    userDetail.Image = appUserDetail.Image;
                    userDetail.Name = appUserDetail.Name;
                    userDetail.Publications = appUserDetail.Publications;
                    userDetail.status = appUserDetail.status;
                }
                else
                {
                    if(! _context.UsersDetail.Where(y => y.Username.ToLower().Trim() == appUserDetail.Username.ToLower().Trim() || y.Email.ToLower().Trim()==appUserDetail.Email.ToLower().Trim()).Any())
                    _context.UsersDetail.Add(appUserDetail);
                    else
                    {

                         ResultReturn.Status_Code = Enums.ResultStatus.AlreadyExists;
                         ResultReturn.Exception=Enum.GetName(typeof(Enums.ResultStatus),Enums.ResultStatus.AlreadyExists);
                ResultReturn.Data=appUserDetail;
            return ResultReturn;

                    }


                }

                _context.SaveChanges();
               
                ResultReturn.Status_Code = Enums.ResultStatus.Success;
                         ResultReturn.Exception=Enum.GetName(typeof(Enums.ResultStatus),Enums.ResultStatus.Success);

                ResultReturn.Data=appUserDetail;

            }
            return ResultReturn;

        }
    }

}
