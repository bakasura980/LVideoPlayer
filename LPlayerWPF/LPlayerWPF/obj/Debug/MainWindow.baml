   M S B A M L   `   `   `  ����  
LPlayerWPF    LPlayerWPF.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -   5          � WindowState$  Normal=�5   	    � Title$ 
MainWindow��5	   	   $	��350��6   $	��425��6)    � Icon$ HeaderIcon.ico��65   .��5
      � .+�5   
   �� -   5         �� TabStripPlacement$	 Top=�5      $��0,0,-0.333,0.333q�6-   .@�5      �� -   5        $��MainTab��5      $��Video Player��6&   $��-3,-1,-0.667,-2.333q�6<   .��5      �  � ColumnDefinitions 5      �� 5      $
��1.7* �6+   �� 5      $��* �6+   �� 5      $��* �6+   �� 5      $��* �6+   �� 5      $��* �6+   �� 5      $��* �6+   �� 5      $��* �6+   �� 5      $
��1.7* �6+    � RowDefinitions 5      �� 5      $	��18* �6(   �� 5      $	��14* �6(   �� 5      $	��85* �6(   �� 5      $	��33* �6(   �� 5      $	��13* �6(   �� 5      $	��12* �6(   �� 5       $	��19* �6(   .+�5%      r� -   5         r�Name$ Video��5%   #    r� UnloadedBehavior$
 Stop=�5&   '    r� LoadedBehavior$ Manual=�5'   '   	 r� Stretch$
	 Fill=�5(   '   
 J� 	AllowDrop
 . $��0��5*   '   $��8��67   $��7��6N    r� Source$1 +C:\Users\Bobo-PC\Desktop\GOT_Best_Scene.mp4?�5+   '   $��0,0,-0.333,0q�6\   �� 5/      -   5         ��Name$
 Home��5/      $��0��6/   $��0��6<   $
��🏰��6L   6_   �� 50      -   5        $ SubSrtct��50      $��0��6/   $��1��6<   $
��Subs��6L   6_   �� 51      -   5        $ OpenBtn��51      $��0��62   $��2��6?   $
��Open��6O   6^   �� 52      -   5        $ none1��52      $��0��6/   $��3��6<   $�� ��6L   �� 53      -	   5        $ none2��53      $��0��6/   $��4��6<   $�� ��6L   �� 54      -
   5        $ SavePhot��54      $��0��6/   $��5��6<   $
��💾��6L   $��17(�6Z   �� 55      -   5        $ ChgTheme��55      $��0��6/   $��6��6<   $
��👀��6L   $
��Bold"�6{   $��17(�6�   �� 56      -   5        $ PlayList��56      $��0��6/   $��7��6<   $��Playlist��6L   $��27��6x   $��0.333,0,-0.333,0q�6�   $��2��6�   $	��Top=�6�   �� 59      -   5         ��Name$ NotificationBlock��59       $��1��5:      $��8��5;      
������ 
������� �� TextAlignment$ Center=�5>      $��17(�5?      $
��Wrap=�5@      $��2��61   5A      �� 5D      -   5        $ SubtitlesText��5D       $��1��5E       $��6��60   $��3��5F       $��2��6-   $�� ��5G       $ Center=�5H       
�������$
��Wrap=�5J       $��21(�5K       
������ 5M      �� 5S      -   5        $ MenuShow��5S      $��6��6/   $��0��6<   $
��Show��6L   6_   �� 5T      -   5        $ None5��5T      $��6��6/   $��1��6<   $�� ��6L   6_   �� 5U      -   5        $ StepBack��5U      $��6��6/   $��2��6<   $	��⏮��6L   6^   �� 5V      -   5        $ Start��5V      $��6��6/   $��3��6<   $	��➤��6L   6^   �� 5W      -   5        $ StepOver��5W      $��6��6/   $��4��6<   $	��⏭��6L   6^   �� 5X      -   5        $ ForwStop��5X      $��6��6/   $��5��6<   $	��❎��6L   6^   �� 5Y      -   5        $
 Mute��5Y      $��6��6/   $��6��6<   $
��📣��6L   $��2��6Y   $��22(�6�   6�   �� 5Z      -   5        $ TimerTxt��5Z       $��4��62   
�������$
��Left=�6R   $��0��6m   $
��Wrap=�6~   $	��Top=�6�   �� 5[      -   5        $ 
textBlock1��5[       $��3��64   $��5��6H   $
��Left=�6X   $��1��6s   $��27.667,45.667,-28.333,0q�6~   $��2��6�   $
��Wrap=�6�   $��	TextBlock��6�   $	��Top=�6�   $	��162��6�   �� 5\      -   5         ��Name$ BalanceSlider��5\      $��6��64   $��2��6D   $��4��6X   $��Stretch=�6e   $	��Top=�6�   $��2��6�    � Value$ 0Q�6�    � Minimum$ -1Q�6�    � Maximum$ 1Q�6�   $y�Visible=�6�   
������ 6  �� 5]      -   5        $ VolumeSlider��5]      $��6��64   $��2��6D   $��5��6X   $��Stretch=�6e   $	��Top=�6�   $��2��6�    J� RenderTransformOrigin$ 0.174,-0.495&�6�   $ 0Q�6�   $ -1Q�6�   $ 1Q�6�   $y�Visible=�6  
������ 
������ 6M  5^      5`      �� 5a      $4��.Enable Voice Recognition                      ��6   $
��Left=�5b      $��23��63   $	��Top=�6?   $	��170��6W   $��-2.333,0,0,0q�6c   .��5c      � ���	#FFE5E5E56   5d      5f      5g      5h      