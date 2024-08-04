using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Rapestry<T> where T : class
    {
        private readonly DpcontextApp dpcontextApp;
        private  DbSet<T> DbSet;

        public Rapestry(DpcontextApp dpcontext) {

            dpcontextApp=dpcontext;
            this.DbSet = dpcontext.Set<T>();
        
        }

        public IEnumerable<T> GetAll(string Propvals)
        {
            IQueryable<T> qeury = DbSet;
            foreach (var line in Propvals.Split(';', StringSplitOptions.RemoveEmptyEntries))
            {
                qeury = qeury.Include(line);
            }
            return qeury;
        }
        public T Get(Expression<Func<T, bool>> expression, string Propvals)
        {
            IQueryable<T> qeury=DbSet;
            qeury=qeury.Where(expression);
            foreach (var line in Propvals.Split(';', StringSplitOptions.RemoveEmptyEntries))
            {
                qeury = qeury.Include(line);
            }
            return qeury.FirstOrDefault<T>();
        }
        public void Update(T entity) { 
            DbSet.Update(entity);
        }
        public void Add(T entity) { 
            dpcontextApp.Add(entity);
        }
        public IQueryable<T> GetSome(Expression<Func<T,bool>> expression )
        {
            IQueryable < T > qeury = DbSet;
            qeury=DbSet.Where(expression);
            return qeury;
        }
    }
}
