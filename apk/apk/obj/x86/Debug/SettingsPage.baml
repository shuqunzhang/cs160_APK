   M S B A M L   `   `   `  ����^  ZMicrosoft.Samples.Kinect.WpfViewers, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null�^clr-namespace:Microsoft.Samples.Kinect.WpfViewers;assembly=Microsoft.Samples.Kinect.WpfViewers#Microsoft.Samples.Kinect.WpfViewers  5   	    apk   apk.SettingsPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   mmy^clr-namespace:Microsoft.Samples.Kinect.WpfViewers;assembly=Microsoft.Samples.Kinect.WpfViewers     5   	   -   5        $	��700��5   	   $
��1080��6     N� Title$  SettingsPage��6#    N� ShowsNavigationUI .   � 	Resources 5       �� 
TargetType �� Property �� Value �� 
TargetType �� Padding �� CornerRadius	 �� Triggers
 P� Property P� Value �� 
TargetName��%O  '��       �� 5   
   " ��6   .�5	      ��  �@�OverridesDefaultStyle . 66   �� 5
       � ��$ Segoe UI)�6+   �� 5       � ��$
 48px(�6)   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� $��border��6!   $��3q�6/   $ 0q�6C   
���   �$ 10��6c   ���#EEEEEE._�5      � .+�5   "   �� $��content��5   8   $��Center=�5   3   $��Center=�5      68   5      5      	 5      P� 5      
 �@�Tag$ hover��65   . �5   "   �� $ border��6)    � �� �#AAAAAA6S   5      P� 5      
 �@�Tag$ press��65   . �5   "   �� $ border��6)    � ��
 �����6S   �� 5   "    � ��
 �����6?   5      P� 5      
 � {� .  . �5   "   ��  �@J�Opacity$	 0.4Q�6<   5      5!      5#      5$      5%      .��5'      �� -   5        $��	cSettings��5'      |�. .+�5(   
   ;   5Microsoft.Samples.Kinect.WpfViewers.KinectColorViewer  -   5        $��kinectColorViewer1��5(   Z    �� Left$ 45��6   
 �� Top$	 107��60   $	��480��6A   $	��360��6M   5   /Microsoft.Samples.Kinect.WpfViewers.ImageViewer   Kinect 6t   ��  �� ElementName$ kinectSensorChooser1�� �� Path$ Kinect�=   7Microsoft.Samples.Kinect.WpfViewers.KinectSensorChooser  5)   
   -   5        $��kinectSensorChooser1��5)   C   $ 0��6!   $
 1000��61   $	��328��6_   �� 5*   
   -   5        $��rangeButton��5*   ^   $	 568��6   $ 40��6#   $��Set Range of Motion��63   $	��150��6Q   $	��500��6q   $��42(�6}   6�   �� 5+   
   -   5        $��gesturesButton��5+   a   $	 568��6   $	 205��6#   $��Manage Taboo Gestures��64   $	��150��6T   $	��500��6w   $��42(�6�   6�   �� 5,   
   -   5        $��wordsButton��5,   ^   $	 568��6   $	 368��6#   $��Manage Taboo Words��64   $	��150��6Q   $	��500��6q   $��42(�6}   6�   �� 5-   
   -   5        $��
homeButton��5-   V   $ 24��6   $	 534��6"   $��Return Home��63   $	��150��6I   $	��500��6h   $��42(�6t   6�   �� 5.   
   -	   5        $��volumeButton��5.   d   $	 568��6   $	 533��6#   $��
Set Volume��64   $��42(�6I   $	��150��6W   $	��500��6x   6�   �� 5/   
   -
   5        $��
textBlock1��5/   A   $ 24��6   $ 12��6%   $��84��65   $��Settings��6S   $��64(�6c   �� 50   
   -   5        $��
savedLabel��50   Y   $	 352��6   $	 196��6"   $��Setting Saved!��63   $	��250��6L   $��56(�6k   $	��400��6y   
�������$y�Hidden=�6�   $��Center=�6�   $��Center=�6�   
�������6�   �� 51   
   -   5        $��volumePanel��51   X   
�������$	 152��6(   $	 470��6:   $	��215��6K   $	��800��6k   $y�Hidden=�6w   .+�52      �� -   5        $��label7��52   |   ���	#FF111111$��Set Desired Volume Level:��6+   $��36(�6O   
�������$��64��6p   6|   �� 53      -   5        $��label8��53   j   $7��1Count to five outloud in the desired volume level��6   $��36(�6P   $��64��6^   $��10q�6x   54      �� 55   
   -   5        $��rightRangePanel��55   E   $	 152��6   $	 470��6'   $	��215��68   $	��800��6\   
�������$y�Hidden=�6{   .+�56      �� -   5        $��label1��56   ?   $��Set Range of Motion:��6   $��64��63   $��36(�6M   ���	#FF111111
�������6r   �� 57      -   5        $��label2��57   V   $1��+Hold your RIGHT hand at your RIGHT boundary��6   $��64��6J   $��36(�6d   $��10q�6r   �� 58      -   5        $��label3��58   G   $��for 3 seconds.��6   $��36(�6-   $��64��6;   $��10q�6U   � 59      -   5        $��rightProgress��59   &   $��25��6   $	��175��6;    � Maximum$
 3000Q�6G   5:      �� 5;   
   -   5        $��leftRangePanel��5;   X   
�������$	 152��6(   $	 470��6:   $	��215��6K   $	��800��6n   $y�Hidden=�6z   .+�5<      �� -   5        $��label4��5<   w   ���	#FF111111$��Set Range of Motion:��6+   $��36(�6J   
�������$��64��6k   6w   �� 5=      -   5        $��label5��5=   b   $/��)Hold your LEFT hand at your LEFT boundary��6   $��36(�6H   $��64��6V   $��10q�6p   �� 5>      -   5        $��label6��5>   G   $��for 3 seconds.��6   $��36(�6-   $��64��6;   $��10q�6U   � 5?      -   5        $��leftProgress��5?   &   $��25��6   $	��175��6:   $
 3000Q�6F   5@      �� 5A   
   -   5        $��panelBorder��5A   !   $	��215��6   
���   �$	��800��6C   $	 152��6O   $	 470��6a   $��3��6r   $y�Hidden=�6�   5B      5C      