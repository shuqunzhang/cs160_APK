   M S B A M L   `   `   `  ����^  ZMicrosoft.Samples.Kinect.WpfViewers, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null�^clr-namespace:Microsoft.Samples.Kinect.WpfViewers;assembly=Microsoft.Samples.Kinect.WpfViewers#Microsoft.Samples.Kinect.WpfViewers  5   	    apk   apk.PresentationPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   mmy^clr-namespace:Microsoft.Samples.Kinect.WpfViewers;assembly=Microsoft.Samples.Kinect.WpfViewers     5   	   -   5        $	��700��5   	   $
��1080��6     N� Title$  PresentationPage��6#   .��5      �� -   5        $��cPresentation��5      |�. .+�5   
   ;   5Microsoft.Samples.Kinect.WpfViewers.KinectColorViewer  -   5        $��kinectColorViewer1��5   W    �� Left$ 0��6   
 �� Top$ 0��6/   $	��900��6>   $	��675��6J   5   /Microsoft.Samples.Kinect.WpfViewers.ImageViewer   Kinect 6q   ��  �� ElementName$ kinectSensorChooser1�� �� Path$ Kinect�=   7Microsoft.Samples.Kinect.WpfViewers.KinectSensorChooser  5	   
   -   5        $��kinectSensorChooser1��5	   C   $ 0��6!   $
 1000��61   $	��328��6_   �� 5
   
   -   5        $��endLabel��5
   �   $ 0��6   $	 652��6    $B��<Hold hands above head for 3 seconds to end the presentation.��61   $��48��6x   $
��1080��6�   $��24(�6�   $��Center=�6�   ���	#FF030303
������� K� ZIndex$ 1��6�   �� 5   
   -   5        $��
startPanel��5      $	 166��6(   $	 122��6:   $	��400��6K   $	��600��6X   $y�Hidden=�6d   ���	#FFEEEEEE.+�5      �� -   5        $��
startLabel��5       $��90��6   $��Begin presentation in:��62   $��48(�6S   $	��600��6a   �� 5      -   5        $��	startTime��5   !   $	��200��6   $��5��62   $	��150(�6>   $	��150��6M   $��Center=�6Y   5      �� 5   
   -	   5        $��	infoPanel��5   C   $	 900��6   $ 0��6'   $	��675��66   $	��180��6T   .+�5      �� -
   5        $��	timeLabel��5   0   $
��0:00��6   $	��122��6#   $��64(�6A   $��Center=�6O   $��Center=�6s   �� 5      -   5        $��postureLabel��5   d   $��Posture��6   $��48(�6&   $��75��64   $��Center=�6@   
�������
�������6�   �� 5      -   5        $��motionLabel��5   b   $��Range��6   $��48(�6$   $��75��62   $��Center=�6>   
�������6u   �� 5      -   5        $��gestureLabel��5   d   $��Gesture��6   $��48(�6&   $��75��64   $��Center=�6@   
�������
�������6�   �� 5      -   5        $��gestureDetail��5   d   $��Gesture��6   $��24(�6&   $��48��64   $��Center=�6@   
�������6y   �� 5      -   5        $��	wordLabel��5   a   $
��Word��6   $��48(�6#   $��75��61   $��Center=�6=   
�������
�������6�   �� 5      -   5        $��
wordDetail��5   a   $
��Word��6   $��24(�6#   $��48��61   $��Center=�6=   
�������6s   �� 5      -   5        $��volumeLabel��5   c   $��Volume��6   $��48(�6%   $��75��63   $��Center=�6?   
�������
�������6�   ;   5Microsoft.Samples.Kinect.WpfViewers.KinectAudioViewer  5      -   5        $��kinectAudioViewer1��5   #   $��63��6=   $	��180��6I    6U   �� $ kinectSensorChooser1��$ Kinect�5      �� 5   
   -   5        $��endPanel��5   D   $	 302��6   $	 612��6'   $��44��68   $	��270��6T    �� Orientation$ 
Horizontal=�6`   $y�Hidden=�6y   .+�5      �� -   5        $��endingLabel��5   2   $��Ending:��6   $��44��6&   $��24(�6E   
���  ��6S   � 5      -   5        $��endProgress��5   &   $��25��6   $	��175��69    � Maximum$
 3000Q�6E   5      5      5      