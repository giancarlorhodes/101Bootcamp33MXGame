

namespace Capstone_Xavier.Controllers
{
    using Capstone_BLL;
    using Capstone_Xavier.Filters;
    using Capstone_Xavier.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    //For admin/gamemaster funtions
    public class AdminController : Controller
    {
        // GET: Admin
        [MustBeLoggedIn]
        [MustBeInRole(Roles="Admin")]
        public ActionResult Admin()
        {
            Mapper map = new Mapper();
            AdminModel admin = new AdminModel();
            DBUse data = new DBUse();

            admin.users = map.UserBO_To_List(data.GetAllUsers());
            foreach (UserModel user in admin.users) {
                user.characters = map.CharacterModel_To_List(data.GetCharacters(user.userID));
            }

            admin.monsters = map.MonsterBO_To_List(data.GetAllMonsters());


            return View(admin);
        }

        [MustBeLoggedIn]
        [MustBeInRole(Roles = "GameMaster")]
        public ActionResult Gamemaster() {
            Mapper map = new Mapper();
            AdminModel admin = new AdminModel();
            DBUse data = new DBUse();

            admin.monsters = map.MonsterBO_To_List(data.GetAllMonsters());
            return View(admin);
        }

        //-------------------------User Manipulation-----------------------

        [HttpPost]
        public ActionResult GetAllUsers() {
            DBUse data = new DBUse();
            Mapper map = new Mapper();

            return View();
        }

        [HttpGet]
        [MustBeLoggedIn]
        [MustBeInRole(Roles = "Admin")]
        public ActionResult ReturnUserInfo(UserModel user) {
            Mapper map = new Mapper();
            DBUse data = new DBUse();

            user.roles = map.RoleBO_To_List(data.GetRoles());

            return View("AdminUserAlter", user);
        }

        [HttpPost]
        public ActionResult UpdateUser(UserModel user) {
            DBUse data = new DBUse();
            Mapper map = new Mapper();

                if (user.password == user.confirmPassword)
                {
                    data.UpdateUserInfo(map.UserModel_To_BO(user));
                    user.alertType = 1;
                }
                else {
                    user.alertType = 2;
                }

            if (user.changeRole == true) {
                data.ChangeUserRole(user.userID, user.roleID);
            }

            return RedirectToAction("Admin", "Admin");
        }

        public ActionResult RemoveUser(UserModel user) {
            DBUse data = new DBUse();

            data.RemoveUser(user.userID);

            return RedirectToAction("Admin", "Admin");
        }

        //----------------------Monster Manipulation------------------------
        [HttpGet]
        [MustBeInRole(Roles = "Admin,GameMaster")]
        [MustBeLoggedIn]
        public ActionResult MonsterAlter(MonsterModel monster) {

            return View(monster);
        }

        [HttpPost]
        public ActionResult MonsterAlterPost(MonsterModel monster) {
            DBUse data = new DBUse();
            Mapper map = new Mapper();
            if (ModelState.IsValid)
            {
                data.UpdateMonster(map.MonsterModel_To_BO(monster));
                monster.alertType = 1;
                return RedirectToAction("MonsterAlter", "Admin", monster);
            }
            else {
                monster.alertType = 2;
                return RedirectToAction("MonsterAlter", "Admin", monster);
            }
            
            
        }

        public ActionResult RemoveMonster(MonsterModel monster) {
            DBUse data = new DBUse();
            string role = Session["Role"].ToString();

            data.RemoveMonster(monster.monsterID);

            if (role == "1")
            {
                return RedirectToAction("Admin", "Admin");
            }
            else {
                return RedirectToAction("GameMaster", "Admin");
            }

            
        }

        [HttpGet]
        [MustBeInRole(Roles ="Admin,GameMaster")]
        [MustBeLoggedIn]
        public ActionResult CreateMonster() {
            MonsterModel monster = new MonsterModel();

            return View(monster);
        }

        [HttpPost]
        public ActionResult CreateMonster(MonsterModel monster) {
            DBUse data = new DBUse();
            Mapper map = new Mapper();
            string role = Session["Role"].ToString();

            if (ModelState.IsValid)
            {
                data.CreateMonster(map.MonsterModel_To_BO(monster));
                if (role == "1")
                {
                    return RedirectToAction("Admin", "Admin", monster);
                }
                else {
                    return RedirectToAction("GameMaster", "Admin", monster);
                }
            }
            else {
                return RedirectToAction("CreateMonster", "Admin", monster);
            }

            

            
        }

    }
}