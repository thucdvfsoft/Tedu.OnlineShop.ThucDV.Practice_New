using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.Dao
{
    public class ContentDao
    {
        private OnlineShopDbContext db = null;
        public ContentDao()
        {
            db = new OnlineShopDbContext();
        }

        public List<Content> ListAll()
        {
            return db.Contents.Where(s => s.Status == true).ToList();
        }

        public long Insert(Content content)
        {
            var ct = db.Contents.Add(content);
            db.SaveChanges();
            return ct.ID;
        }

        public Content ViewDetail(long id)
        {
            return db.Contents.Find(id);
        }

        public bool Update(Content entity)
        {
            var content = db.Contents.Find(entity.ID);
            content.CategoryID = entity.CategoryID;
            content.Detail = entity.Detail;
            content.Image = entity.Image;
            content.MetaDescriptions = entity.MetaDescriptions;
            content.MetaKeyWord = entity.MetaKeyWord;
            content.MetaTitle = entity.MetaTitle;
            content.ModifiedDate = DateTime.Now;
            content.MoreImages = entity.MoreImages;
            content.Name = entity.Name;
            content.Tags = entity.Tags;
            content.TopHot = entity.TopHot;
            content.ViewCount = entity.ViewCount;
            content.Warranty = entity.Warranty;

            db.SaveChanges();
            return true;
        }

        public Content GetById(long id)
        {
            return  db.Contents.Find(id);
        }
    }
}
