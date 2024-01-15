using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ItemStrategy
{
    public abstract void Use(Player player);
}
//ȸ�� �ɷ�
public class HealItemStractegy : ItemStrategy
{
    const int healHpPoint = 30;
    public override void Use(Player player)
    {
        player.Hp += healHpPoint;
    }
}
//�߰� point ȹ�� �ɷ�
public class PointItemStractegy : ItemStrategy
{
    public override void Use(Player player)
    {
        int randPoint = Random.Range(1, 4);
        GameManager.instance.Point += randPoint;
    }
}
//ź�� ���� �ɷ�
public class AmmoItemStractegy : ItemStrategy
{
    public override void Use(Player player)
    {
        int count = Random.Range(15, 31);
        if(player.TotalBulletCount == player.maxTotalBulletCount)
            player.weapon.BulletCount += count;
        else
            player.TotalBulletCount += count;
    }
}
public class Item : MonoBehaviour
{
    public enum ItemMode
    {
        heal,
        point,
        ammo
    }
    public ItemMode itemMode;

    ItemStrategy curItemStractegy = null;
    void Start()
    {
        ItemModeCheck();
    }
    void ItemModeCheck()
    {
        switch(itemMode)
        {
            case ItemMode.heal:
                curItemStractegy = new HealItemStractegy();
                break;
            case ItemMode.point:
                curItemStractegy = new PointItemStractegy();
                break;
            case ItemMode.ammo:
                curItemStractegy = new AmmoItemStractegy();
                break;
        }
    }
    public virtual void ItemStractegy(Player player)
    {
        Debug.Log("������ ȿ��~");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Player player))
        {
            curItemStractegy.Use(player);
            SoundManager.instance.SFXPlay("ItemGet", SoundManager.instance.SPXclips[4], transform);
            Destroy(gameObject);
        }
    }

}
