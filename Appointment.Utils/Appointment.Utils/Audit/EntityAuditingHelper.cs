using Appointment.Utils.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Appointment.Utils.Audit;

public static class EntityAuditingHelper
    {
        public static void SetCreationAuditProperties(
            object entryAsObj,
            long? userId)
        {
            var entry = entryAsObj as EntityEntry;
            object entityAsObj = entry.Entity;
            var entityWithCreationTime = entityAsObj as IHasCreationTime;
            if (entityWithCreationTime == null)
            {
                //Object does not implement IHasCreationTime
                return;
            }

            if (entityWithCreationTime.CreationTime == default(DateTime))
            {
                entityWithCreationTime.CreationTime = DateTime.Now;
            }

            if (!(entityAsObj is ICreationAudited))
            {
                //Object does not implement ICreationAudited
                return;
            }

            if (!userId.HasValue)
            {
                //Unknown user
                return;
            }

            var entity = entityAsObj as ICreationAudited;
            if (entity.CreatorUserId != null)
            {
                //CreatorUserId is already set
                return;
            }

            //Finally, set CreatorUserId!
            entity.CreatorUserId = userId;
        }

        public static void SetModificationAuditProperties(
            object entryAsObj,
            long? userId)
        {
            var entry = entryAsObj as EntityEntry;
            object entityAsObj = entry.Entity;
            if (entityAsObj is IHasModificationTime)
            {
                entityAsObj.As<IHasModificationTime>().LastModificationTime = DateTime.Now;
            }

            if (!(entityAsObj is IModificationAudited))
            {
                //Entity does not implement IModificationAudited
                return;
            }

            var entity = entityAsObj.As<IModificationAudited>();

            if (userId == null)
            {
                //Unknown user
                entity.LastModifierUserId = null;
                return;
            }

            //Finally, set LastModifierUserId!
            entity.LastModifierUserId = userId;
        }

        public static void SetDeletionAuditProperties(
            object entryAsObj,
            long? userId)
        {
            var entry = entryAsObj as EntityEntry;
            object entityAsObj = entry.Entity;
            if (!(entityAsObj is ISoftDelete))
            {
                //Entity does not implement IDeletionAudited
                return;
            }

            entry.State = EntityState.Unchanged;

            var entity = entityAsObj.As<ISoftDelete>();
            entity.DeletionTime = DateTime.Now;
            entity.IsDeleted = true;
            if (userId == null)
            {
                //Unknown user
                entity.DeleterUserId = null;
                return;
            }

            //Finally, set LastModifierUserId!
            entity.DeleterUserId = userId;
        }
    }