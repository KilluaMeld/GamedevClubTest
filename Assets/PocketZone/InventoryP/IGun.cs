using System;

public interface IGun
{
    public Type Bullets { get;}
    public float ShootingSpeed { get;}
    public float Damage { get;}
    public int MagazineCapacity { get;}
    public bool Shoot(InventoryWithSlots inventory);
    
}

