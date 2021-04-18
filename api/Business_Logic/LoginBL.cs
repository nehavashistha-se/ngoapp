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
        ResultReturn ResultReturn = new ResultReturn();

        public ResultReturn GetLogindetails(Appusers appuser)
        {

            var User = _context.Users.Where(y => y.Username == appuser.Username && y.Password == appuser.Password).FirstOrDefault();
            if (User == null)
            {
                ResultReturn.Status_Code = Enums.ResultStatus.InvalidLogin;
            }
            else
            {
                ResultReturn.Id = User.UserId;
                ResultReturn.Status_Code = Enums.ResultStatus.Success;

            }

            return ResultReturn;

        }


        public ResultReturn SaveUser(Appusers appuser)
        {
            using (_context)
            {
                var UserExist = _context.Users.Where(y => y.Username == appuser.Username && y.Password == appuser.Password).Any();
                if (UserExist)
                {
                    var userdata = _context.Users.Where(y => y.Username == appuser.Username && y.Password == appuser.Password).FirstOrDefault();
                    userdata.Username = appuser.Username.Trim();
                    userdata.Password = appuser.Password.Trim();
                    userdata.Role = appuser.Role.Trim();

                    ResultReturn.Status_Code = Enums.ResultStatus.Success;
                }
                else
                {
                    _context.Users.Add(appuser);
                    ResultReturn.Status_Code = Enums.ResultStatus.Success;

                }
                _context.SaveChanges();
            }
            return ResultReturn;

        }

        public ResultReturn SaveUserDetail(AppUserDetail appUserDetail)
        {
            using (_context)
            {
                var UserExist = _context.UsersDetail.Where(y => y.UserId == appUserDetail.UserId).Any();
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
                    _context.UsersDetail.Add(appUserDetail);


                }

                _context.SaveChanges();
                ResultReturn.Status_Code = Enums.ResultStatus.Success;

            }
            return ResultReturn;

        }
    }

}
