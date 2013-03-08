using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taiko_Combo_Assigner
{
    public partial class Form1
    {
        const int SameComboCircle = 1;
        const int ComboVariation = 5;
        //from here it is 5 + 16*(combosMoved - 1) or 16*(combosMoved) - 11
        const int SliderVariation = 1; //+1 to all things dealing with sliders
        const int SpinnerVariation = 12; //at 12, they count as a new combo, but they don't change the combo color, if you change the combo color, x mod 16 = 12 means that it is a spinner
        const int ComboMultiplier = 16; //the amount you move for each new combo
        //hitsounds (you can think about it like a 4-bit binary number)
        //I could make this more robust, but it should be okay for now
        const int none = 0;
        const int whistle = 2;
        const int finish = 4;
        const int clap = 8;
        const int whistleFinish = 6;
        const int whistleClap = 10;
        const int clapFinish = 12;
        const int whistleClapFinish = 14;
        //hitobject information
        const int comboSlot = 3;
        const int hitSoundSlot = 4;
        //type of hitObjects
        const int red = 1;
        const int blue = 2;
        const int bigRed = 3;
        const int bigBlue = 4;
        const int slider = 5;
        const int finishSlider = 6;
        const int spinner = 7;
    }
}
