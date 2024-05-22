﻿namespace Store.Domain.Entities
{
    public abstract class Entity
    {
        public Entity()
        {
            Id = new Guid();
        }

        public Guid Id { get; private set; }
    }
}
