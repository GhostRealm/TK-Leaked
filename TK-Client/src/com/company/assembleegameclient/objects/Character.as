package com.company.assembleegameclient.objects
{
import com.company.assembleegameclient.sound.SoundEffectLibrary;

public class Character extends GameObject
{


   public var hurtSound_:String;

   public var deathSound_:String;

   public function Character(objectXML:XML)
   {
      super(objectXML);
      this.hurtSound_ = Boolean(objectXML.hasOwnProperty("HitSound"))?String(objectXML.HitSound):"monster/default_hit";
      SoundEffectLibrary.load(this.hurtSound_);
      this.deathSound_ = Boolean(objectXML.hasOwnProperty("DeathSound"))?String(objectXML.DeathSound):"monster/default_death";
      SoundEffectLibrary.load(this.deathSound_);
   }

   public static function green2redu(param1:uint) : uint
   {
      if(param1 > 50)
      {
         return 65280 + 327680 * (100 - param1);
      }
      return 16776960 - 1280 * (50 - param1);
   }

   override public function damage(origType:int, damageAmount:int, effects:Vector.<uint>, kill:Boolean, proj:Projectile) : void
   {
      super.damage(origType,damageAmount,effects,kill,proj);
      if(dead_)
      {
         SoundEffectLibrary.play(this.deathSound_);
      }
      else
      {
         SoundEffectLibrary.play(this.hurtSound_);
      }
   }
}
}
