using API.Contexts;
using API.Models;
using API.ViewModels;
using API.Handlres;

namespace API.Repository.Data;

public class AccountRepositories : GeneralRepository<MyContext, Account,int>
{
    private readonly MyContext _context;

    public AccountRepositories(MyContext context) : base(context)
    {
        _context = context;
    }

    public int Register(RegisterVM register)
    {
        int id = GenerateId();
        if (!CheckEmailPhone(register.Email, register.Phone))
        {
            return 0; // Email atau Password sudah terdaftar
        }

        if (!IsPasswordValid(register.Password))
        {
            return 3; // Password Must Contain blabla
        }

        Employee employee = new Employee()
        {
            Id = id,
            Phone = register.Phone,
            Gender = register.Gender,
            BirthDate = register.BirthDate,
            Image = register.Image,
            Email = register.Email,
            ManagerId = null
        };
        int index = register.FullName.IndexOf(" ");
        if (index <= 0)
        {
            employee.FirstName = register.FullName;
            employee.LastName = register.FullName;
        }
        else
        {
            employee.FirstName = register.FullName.Substring(0, register.FullName.IndexOf(" "));
            employee.LastName = register.FullName.Substring(register.FullName.IndexOf(" ") + 1);
        }

        _context.Employees.Add(employee);
        _context.SaveChanges();

        Account account = new Account()
        {
            Id = id,
            Password = Hashing.HashPassword(register.Password),
        };
        _context.Accounts.Add(account);
        _context.SaveChanges();

        /*AccountRole accountRole = new AccountRole()
        {
            AccountId = id,
            RoleId = 3
        };
        _context.AccountRoles.Add(accountRole);
        _context.SaveChanges();*/

        return 1;
    }

    public int Login(LoginVM login)
    {
        var result = _context.Accounts.Join(_context.Employees, a => a.Id, e => e.Id, (a, e) => new LoginVM
        {
            Email = e.Email,
            Password = a.Password
        }).SingleOrDefault(c => c.Email == login.Email);

        if (result == null)
        {
            return 0; // Email not found
        }
        else if (!Hashing.ValidatePassword(login.Password, result.Password))
        {
            return 1; // Wrong Password
        }
        return 2; // Email & Pass true
    }

    public bool IsPasswordValid(string password)
    {
        return password.Length >= 8 &&
               password.Length <= 15 &&
               password.Any(char.IsDigit) &&
               password.Any(char.IsLetter) &&
               (password.Any(char.IsSymbol) || password.Any(char.IsPunctuation));
    }

    private bool CheckEmailPhone(string email, string phone)
    {
        var duplicate = _context.Employees.Where(s => s.Email == email || s.Phone == phone).ToList();

        if (duplicate.Any())
        {
            return false; // Email atau Password sudah ada
        }
        return true; // Email dan Password belum terdaftar
    }

    private int GenerateId()
    {
        var empCount = _context.Employees.OrderByDescending(e => e.Id).FirstOrDefault();

        if (empCount == null)
        {
            return 1;
        }
        string Id = empCount.Id.ToString();
        return Convert.ToInt32(Convert.ToInt32(Id) + 1);
    }

    public List<string> UserRoles(string email)
    {
        var getId = _context.Employees.SingleOrDefault(e => e.Email == email).Id;
        var getRoles = _context.AccountRoles.Where(ar => ar.AccountId == getId)
                                                .Join(_context.Roles, ar => ar.RoleId, r => r.Id, (ar, r) => r.Name)
                                                .ToList();
        return getRoles;
    }
}
