using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class UnitOfWork
    {
        private readonly DpcontextApp appContext;
        public Rapestry<Catergory> Catergory { get; set; }
        public Rapestry<AppUser> User { get; set; }
        public Rapestry<Post> Post { get; set; }
        public Rapestry<WatchHistory> WatchHistory { get; set; }
        public Rapestry<RecomendCategories> RecomendeCategories { get; set; }
        public UnitOfWork(DpcontextApp appContext)
        {
            this.appContext = appContext;
            this.Catergory = new Rapestry<Catergory>(appContext);
            this.User = new Rapestry<AppUser>(appContext);
            this.Post=new Rapestry<Post>(appContext);
            this.WatchHistory=new Rapestry<WatchHistory>(appContext);
            this.RecomendeCategories = new Rapestry<RecomendCategories>(appContext);
        }
        
        public void Save()
        {
            appContext.SaveChanges();  
        }
    }
}
