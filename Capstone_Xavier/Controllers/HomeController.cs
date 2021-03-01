
namespace Capstone_Xavier.Controllers
{
    using Capstone_BLL;
    using Capstone_BLL.BusinessObjects;
    using Capstone_Xavier.Filters;
    using Capstone_Xavier.Models;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Web.Security;

    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {

            return View();
        }
        //---------------User Creation----------------------

        [HttpGet]
        public ActionResult Register() {

            RegisterModel register = new RegisterModel();

            return View(register);
        }

        [HttpPost]
        public ActionResult Register(RegisterModel register) {


            if (ModelState.IsValid)
            {
                Mapper mapper = new Mapper();

                UsersBO user = mapper.UIRegister_To_BO(register);
                DBUse data = new DBUse();

                //For checking if the username already exist
                if (data.FindUsername(register.username) == false)
                {
                    //For validating passwords. 
                    if (ValidatePassword(register.password)) {
                        data.AddUser(user);
                        LoginModel login = new LoginModel();
                        login.alertType = 3;
                        //For getting the userdata in creating the default character to use
                        UsersBO _user = data.FindUser(user);
                        CreateDefaultCharacter(_user.UserID);

                        return RedirectToAction("Login", "Home");
                    } else {
                        //If the password doesnt meet the requirements
                        register.alertType = 3;
                        return View(register);
                    }

                }
                else {
                    //If the username already exist in the database
                    register.alertType = 2;
                    return View(register);
                }

            }
            else {//For if the modelstate isnt valid
                if (register.username == null) {
                    register.userValid = 1;
                }
                if (register.password == null) {
                    register.passValid = 1;
                }
                if (register.email == null) {
                    register.emailValid = 1;
                }
                register.alertType = 1;
                return View(register);
            }

        }
        //----------------User login---------------------

        [HttpGet]
        public ActionResult Login() {

            LoginModel _loginModel = new LoginModel();

            _loginModel.username = "";
            _loginModel.password = "";

            return View(_loginModel);
        }

        [HttpPost]
        public ActionResult Login(LoginModel login) {
            Mapper mapper = new Mapper();

            UsersBO user = mapper.UILogin_To_BO(login);
            DBUse data = new DBUse();
            user = data.FindUser(user);
            if (ModelState.IsValid)
            {
                if (user.Username == null )
                {
                    login.alertType = 2;
                    return View(login);
                }
                else {
                    Session["Username"] = user.Username;
                    //user doesnt return password for security. Pass in login pass for use later.
                    user.Password = login.password;
                    Session["User"] = mapper.UserBO_To_Model(user);
                    Session["Role"] = user.UserRole.ToString();
                    Session["UserID"] = user.UserID;
                    return RedirectToAction("Users", "Home");
                }

            }
            else {
                login.alertType = 1;
                return View(login);
            }

        }

        [MustBeLoggedIn]
        public ActionResult Test() {
            return View();
        }

        //For sendin users to the home page. 
        [MustBeLoggedIn]
        [HttpGet]
        public ActionResult Users() {
            UserModel user = new UserModel();
            Mapper mapper = new Mapper();
            DBUse data = new DBUse();
            int userID = (int)Session["UserID"];

            List<CharacterModel> characters = mapper.CharacterModel_To_List(data.GetCharacters(userID));
             

            return View(characters);
        }


        [HttpGet]
        public ActionResult Logout() {
            FormsAuthentication.SignOut();
            Session.Abandon();
    
            return RedirectToAction("Index", "Home");
        }

        //-------------Misc-------------
        private bool ValidatePassword(string password) {
            bool _returnBool = false;
            char[] _password = password.ToCharArray();
            int numcount = 0;
            int specialCount = 0;
            int capCount = 0;
            int count = _password.Length;

            for (int i = 0; i < _password.Length; i++) {
                char temp = _password[i];
                if (char.IsLetterOrDigit(temp))
                {
                    if (char.IsDigit(temp))
                    {
                        numcount++;
                    }
                    else {
                        if (char.IsUpper(temp)) {
                            capCount++;
                        }
                    }
                }
                else {
                    specialCount++;
                }
            }

            if (numcount >= 1 && capCount >= 1 && specialCount >= 1 && count >= 8 && count <= 20)
            {
                _returnBool = true;
            }
            else {
                _returnBool = false;
            }


            return _returnBool;
        }

        public ActionResult FAQS() {
            return View();
        }

        private void CreateDefaultCharacter(int userID) {
            DBUse data = new DBUse();
            Mapper map = new Mapper();
            CharacterModel character = new CharacterModel();
            ClassModel _class = map.ClassBO_To_Model(data.GetClassInfo(4));

            character.health = _class.baseHP;
            character.userID = userID;
            character.name = "Default";
            character.classID = 4;
            

            data.CreateCharacter(map.CharacterModel_To_BO(character));
        }
    }
}