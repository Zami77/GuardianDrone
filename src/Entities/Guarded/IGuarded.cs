using Godot;
using System;

public interface IGuarded
{
    void TakeDamage(int dmg);
    bool IsDead();
}