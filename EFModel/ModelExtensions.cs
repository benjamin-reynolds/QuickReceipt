using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Text;

namespace Repository.Model
{
    public static class ModelExtensions
    {
        #region Public Static Members

        /// <summary>
        /// This method attempts to find a Entity in the context object cache and
        /// return it if found.  If Entity is not found, then a dummy instance is
        /// attached to the context.  Please note that attaching a object to the
        /// context does not add it to the database.  It only informs the context
        /// of a existing entity that already exist in the database
        /// </summary>
        /// <returns></returns>
        public static E FindOrAttachEntity<E>(this ObjectContext context, E entityToFind)
            where E : IEntityWithKey
        {
            E entity = entityToFind;
            ObjectStateEntry entry = null;

            if (context.ObjectStateManager.TryGetObjectStateEntry(entityToFind.EntityKey, out entry))
            {
                if (entry.Entity == null)
                {
                    context.Attach(entity);
                }
                else
                {
                    entity = (E)entry.Entity;
                }
            }
            else
            {
                context.Attach(entity);
            }

            return entity;
        }

        /// <summary>
        /// This method attempts to find a Entity in the context object cache and
        /// return it if found.  If Entity is not found, then a dummy instance is
        /// attached to the context.  Please note that attaching a object to the
        /// context does not add it to the database.  It only informs the context
        /// of a existing entity that already exist in the database
        /// </summary>
        /// <typeparam name="E">The Entity to return</typeparam>
        /// <typeparam name="K">The Entity Key type</typeparam>
        /// <param name="entitySetName">The Entity Set Name</param>
        /// <param name="entityKeyName">The Entity Key Name</param>
        /// <param name="key">The Entity Key</param>
        public static E FindOrAttachEntity<E, K>(this ObjectContext context, string entityName, string entityKeyName, K key)
             where E : IEntityWithKey, new()
        {
            string qualifiedEntityName = string.Format("{0}.{1}", context.DefaultContainerName, entityName);

            E entity = CreateEntityInstance<E, K>(qualifiedEntityName, entityKeyName, key);

            return context.FindOrAttachEntity(entity);
        }


        public static E FindOrAttachEntity<E, K>(this ObjectContext context, K key)
            where E : IEntityWithKey, new()
        {
            return context.FindOrAttachEntity<E, K>(typeof(E).Name, "Id", key);
        }
        #endregion

        #region Private Static Members

        private static E CreateEntityInstance<E, K>(string qualifiedEntityName, string keyName, K key)
            where E : IEntityWithKey, new()
        {
            E entity = new E();

            //set key via reflection
            entity.GetType().GetProperty(keyName).SetValue(entity, key, null);
            //set key for entity
            entity.EntityKey = new System.Data.EntityKey(qualifiedEntityName, keyName, key);

            return entity;
        }

        #endregion

    }
}
