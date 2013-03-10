This program automates associating taiko combo colors with their associative hitobjects.

Combos are associated in this order:
Key: (# of combo color). (Taiko representation) = (Code Representation)
1. d = red
2. k = blue
3. D = bigRed
4. K = bigBlue
5. slider = slider
6. finish slider = finishSlider

To use this program, put the contents of your .osu in the topbox, and press process. The bottom box should then be populated with our new combo-assigned .osu file. In the future, this process will hopefully become more intuitive

If you have any questions about the program or suggestions for improvements please drop by http://osu.ppy.sh/forum/t/121948/
=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~

Known Bugs:
	Combo Colors shift by 1 color, after a spinner where both snotes directly before and after the spinner need the same combo color. (http://osu.ppy.sh/forum/t/121687)

=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~
Changelog: 

1.1
	Fixed an issue where if the first note was not a d, combos will be miscolored
	Sort of fixed the issue where the combo will screw up if the combo on both sides of the spinner should be the same (http://osu.ppy.sh/forum/t/121687)
	Clerical Work

1.0
	Initial Release