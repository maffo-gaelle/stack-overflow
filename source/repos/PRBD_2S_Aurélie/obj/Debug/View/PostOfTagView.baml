   M S B A M L   `   `   `  ����K  GPRBD_2S_Aurélie, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:PRBD_2S_Aurélie.ViewPRBD_2S_Aurélie.View  5      I EPRBD_Framework, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG4clr-namespace:PRBD_Framework;assembly=PRBD_FrameworkPRBD_Framework 5      I)clr-namespace:PRBD_2S_Aurélie.PropertiesPRBD_2S_Aurélie.Properties  5      )    #PRBD_2S_Aurélie.View.PostOfTagView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:PRBD_2S_Aurélie.View     5      Bf4clr-namespace:PRBD_Framework;assembly=PRBD_Framework     5      7p)clr-namespace:PRBD_2S_Aurélie.Properties     5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5	      -   5        $  PRBD_Framework.UserControlBase   Name$
  root��5
      .��5      � ���#E0F8F7 � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� .+�5      �� $��0,20,0,0q�6   ���#E0F8F7�� 5      +   %PRBD_2S_Aurélie.Properties.Resources   
PostsOfTag#�Z 6   $��16(�6?   $
��Bold"�6M    �� 5      
�������$
��Bold"�6K   $��16(�6]   �6   �� *Ttag.TagName+6]   5      �� 5      	 �� X1$ 2��6   	 �� Y1$ 2��6   	 �� X2$	 750��6!   	 �� Y2$ 2��6*   
���   �$��7��6@   $��2��6T   $��0,0,0,40q�6a   �� 5      -   5         ��Name$ listVie��5      
������  �� SelectionMode$ Single=�6B   $	��530��5      ���#E0F8F7$
��Auto=�6:   $��3��6l   ��5   Y   �� *Posts+.  (System.Windows.Interactivity.Interaction	   Triggers	 5      /  )System.Windows.Interactivity.EventTrigger  5      
   	EventName$
 MouseDoubleClick��6%   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 60   �� *
ShowPost+   CommandParameter 6M   ��  �� ElementName$ listVie�� �� Path$ SelectedItem�5      ��5       �� 5!      .R�5"      �  � Orientation$ Vertical=�6$   6=   5#      ��5%      �� 5&      .R�5'      �� 
���   �$��3q�65   $	��720��6I   ._�5(      ]� $��0,14,0,0q�6(   .+�5)   "   �  5*   &   �� 5+   *   $
��Auto �68   �� 5,   *   $
��Auto �68   �� 5-   *   $
��Auto �68   .+�5/   &   �� $
��auto��60    �� TextAlignment$
 Left=�6>   $
��Left=�6S   $��1��6n   $
��0,10q�6{   �� 50   *   $
��Bold"�6j   $��16(�6|   �6.   �� *Title+ �� UpdateSourceTrigger$ PropertyChanged=�6|   52   '   �� 53   &   $��3��60   $
��0,10q�6=   �� 54   *   $��12(�6t   $
��Bold"�6�   $��Italic$�6�   ���#007FFF�6.   �� *Author.FullName+$ PropertyChanged=�6�    �� 55   *   $��10(�6n   $��DemiBold"�6|   �6.   �� *	Timestamp+$ PropertyChanged=�6|    �� 56   *   $�(��6.   $��10(�67   $��DemiBold"�6E    �� 57   *   $��10(�6I   $��DemiBold"�6W   �6.   �� *	NbAnswers+6W    �� 58   *      ListPosts_nbAnswers#�Z 6.   $��DemiBold"�6`   $��10(�6v    �� 59   *   $��10(�6E   $��DemiBold"�6S   �6.   �� *Score+6S    �� 5:   *      ListPosts_score#�Z 6.   $��DemiBold"�6\   $��10(�6r    �� 5;   *   $ 	listView3��63   
������ $ Single=�6~   $��2��6�   $
��Auto��6�   ���#E0F8F7��6`   �� *Tags+��5<   .   �� 5=   2   .R�5>   6   � 6B   5?   3   ��5A   .   �� 5B   2   .R�5C   6   �� �� 5D   :    �� Command 6D   �� *AffichePostsTag+$
 root�� �� CommandParameter 6z   �� .�6�   �� $��DemiBold"�6�   $��12(�6�   �6�   �� *	TagName+6�   6�   5E   7   5F   7   5H   +   5I   '   5J   #   5K      5L      5M      5O      5P      5Q      5R      