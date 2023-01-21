package com.company.assembleegameclient.util
{
import com.company.assembleegameclient.misc.DefaultLabelFormat;
import com.company.assembleegameclient.misc.UILabel;
import com.company.assembleegameclient.ui.tooltip.TooltipHelper;
import flash.filters.DropShadowFilter;
import com.company.assembleegameclient.util.FilterUtil;

public class TierUtil
{


    public function TierUtil()
    {
        super();
    }

    public static function getTierTag(xml:XML, size:int = 16) : UILabel
    {
        var label:UILabel = null;
        var color:Number = NaN;
        var tierTag:String = null;
        var isnotpet:* = !isPet(xml);
        var consumable:* = !xml.hasOwnProperty("Consumable");
        var noTierTag:* = !xml.hasOwnProperty("NoTierTag");
        var treasure:* = !xml.hasOwnProperty("Treasure");
        var petFood:* = !xml.hasOwnProperty("PetFood");
        var tier:Boolean = xml.hasOwnProperty("Tier");
        if(isnotpet && consumable && treasure && petFood && noTierTag)
        {
            label = new UILabel();
            if(tier)
            {
                color = 16777215;
                tierTag = "T" + xml.Tier;
            }
            else if(xml.hasOwnProperty("Rare")){
                color = 0x9900ff;
                tierTag = "R";
            }
            else if(xml.hasOwnProperty("@setType"))
            {
                color = 0xb2bdd6;
                tierTag = "U";
            }
            else if(xml.hasOwnProperty("SetTier"))
            {
                color = 0xb2bdd6;
                tierTag = "U";
            }
            else if (xml.hasOwnProperty("SNormal"))
            {
                color = 0xb2bdd6;
                tierTag = "U";
            }
            else if (xml.hasOwnProperty("SPlus"))
            {
                color = 0xb2bdd6;
                tierTag = "U";
            }
            else if (xml.hasOwnProperty("Legendary"))
            {
                color = 0xffcc66;
                tierTag = "L";
            }
            else if (xml.hasOwnProperty("Revenge") || xml.hasOwnProperty("Mythical"))
            {
                color = 0x9b111e;
                tierTag = "M";
            }
            else if (xml.hasOwnProperty("Eternal"))
            {
                color = 0x98ff98;
                tierTag = "E";
            }
            else
            {
                color = 0xb2bdd6;
                tierTag = "U";
            }
            label.text = tierTag;
            DefaultLabelFormat.tierLevelLabel(label,size,color);
            return label;
        }
        return null;
    }

    public static function isPet(itemDataXML:XML) : Boolean
    {
        var activateTags:XMLList = null;
        activateTags = itemDataXML.Activate.(text() == "PermaPet");
        return activateTags.length() >= 1;
    }
}
}
