using API.Contexts;
using API.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace API.Repository;


public class GeneralRepository<Context, TEntity,T> : IRepository<TEntity,T>
where TEntity : class
where Context : MyContext
{
    private DbSet<TEntity> _setcon;
    private MyContext _context;
    public GeneralRepository(MyContext context)
    {
        _context = context;
        _setcon = context.Set<TEntity>();
    }
    public int Delete(T id)
    {
        var data = _setcon.Find(id);
        if (data == null)
        {
            return 0;
        }
        _setcon.Remove(data);
        var result = _context.SaveChanges();
        return result;
    }

    public IEnumerable<TEntity> Get()
    {
        return _setcon.ToList();
    }

    public TEntity Get(T id)
    {
        return _setcon.Find(id);
    }

    public int Insert(TEntity entity)
    {
        _setcon.Add(entity);
        var result = _context.SaveChanges();
        return result;
    }

    public int Update(TEntity entity)
    {
        _setcon.Update(entity).State = EntityState.Modified;
        var result = _context.SaveChanges();
        return result;
    }
}

