﻿namespace Core.Domain;

public interface IUnitOfWork
{
    public Task Compalete();
}