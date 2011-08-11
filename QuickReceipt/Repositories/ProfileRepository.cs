using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using QuickReceipt.Mappers;
using QuickReceipt.Models;

namespace QuickReceipt.Repositories
{
    public class ProfileRepository
    {
        private ProfileMapper Mapper = new ProfileMapper();

        public List<Profile> List()
        {
            EFModel.QuickReceiptEntities ctx = new EFModel.QuickReceiptEntities();
            
            var profiles = (from p in ctx.Profiles
                            select p).ToList();

            return Mapper.Map(profiles);
        }

        public Profile Save(Profile profile)
        {
            EFModel.QuickReceiptEntities ctx = new EFModel.QuickReceiptEntities();
            var dbProfile = Mapper.Map(profile);

            if (profile.Id > 0)
            {
                //update
                var existingProfile = (from p in ctx.Profiles
                                       where p.Id == profile.Id
                                       select p).FirstOrDefault();

                if (existingProfile == null)
                {
                    throw new ObjectNotFoundException("Profile with Id " + profile.Id.ToString() + " couldn't be found in the database.  No update was peformed");
                }

                ctx.ApplyCurrentValues<EFModel.Profile>("Profiles", dbProfile);
            }
            else
            {
                ctx.Profiles.AddObject(dbProfile);
            }

            ctx.SaveChanges();

            return Mapper.Map(dbProfile);
        }

        public void Delete(int Id)
        {
            EFModel.QuickReceiptEntities ctx = new EFModel.QuickReceiptEntities();

            var existingProfile = (from p in ctx.Profiles
                                   where p.Id == Id
                                   select p).FirstOrDefault();

            if (existingProfile == null)
            {
                throw new ObjectNotFoundException("Profile with Id " + Id.ToString() + " couldn't be found in the database.  No deletion was peformed");
            }

            ctx.DeleteObject(existingProfile);
            ctx.SaveChanges();
        }
    }
}
