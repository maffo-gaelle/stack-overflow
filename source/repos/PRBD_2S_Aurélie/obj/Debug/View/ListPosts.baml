   M S B A M L   `   `   `  ����K  GPRBD_2S_Aurélie, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:PRBD_2S_AuréliePRBD_2S_Aurélie  5      I EPRBD_Framework, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG4clr-namespace:PRBD_Framework;assembly=PRBD_FrameworkPRBD_Framework 5      I)clr-namespace:PRBD_2S_Aurélie.PropertiesPRBD_2S_Aurélie.Properties  5	           PRBD_2S_Aurélie.ListPosts   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:PRBD_2S_Aurélie     5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      <f4clr-namespace:PRBD_Framework;assembly=PRBD_Framework  5      1p)clr-namespace:PRBD_2S_Aurélie.Properties  5	      -   5        $  PRBD_Framework.UserControlBase   Name$
  root��5   7   .��5      �� ���#E0F8F7.+�5   
   ��  �� Orientation$ 
Horizontal=�6   ���#E0F8F7$��0,0,0,0q�6C   .+�5      o� .@�5      m� +   %PRBD_2S_Aurélie.Properties.Resources   ListPosts_Newest#��Z 6   $��14(�6�   $
��Bold"�6�   $��Italic$�6�   $��68��6�   $	��6,0q�6�    m� Command 6L   �� *Newest+ �� UpdateSourceTrigger$ PropertyChanged=�6�   m� 5         ListPosts_vote#��Z 6   $��57��6�   $��14(�6�   $
��Bold"�6�   $��Italic$�6�    6J   �� *Vote+$ PropertyChanged=�6�   m� 5         ListPosts_unanswered#��Z 6   $��14(�6�   $
��Bold"�6�    6P   �� *
Unanswered+$ PropertyChanged=�6�   m� 5         ListPosts_active#��Z 6   $	��140��6�   $��14(�6�   $
��Bold"�6�   $��0q�6�    6L   �� *Active+$ PropertyChanged=�6�   �� 5      $	��120��6W   $��	200,0,0,0q�6c   ��6   �� *Filter+$ PropertyChanged=�6c   �� 5      $��Clear��6   $��60��6I   $��0,0,0,0q�6T   $
��Left=�6e   $
��Bold"�6�   ��6)   �� *ClearFilter+6�   5      5      �� 5   
   -   5         ��Name$ btnNew��5      	   ListPosts_NewQuestion#��Z	 6!   $��16(�6�   $	��170��6�   $��Right=�6�   $��	0,20,20,0q�6�   
 J� RenderTransformOrigin$
 0.406,-0.16&�6�   $��32��6	  y�6Y   �� *ConnectUser+��6|   �� *NewQuestion+6	  �� 5   
   -   5         ��Name$ listView��5      
������  �� SelectionMode$ Single=�6?   $	��530��5      ���#E0F8F7$
��Auto=�66   ��5   V   �� *Posts+$ PropertyChanged=�.  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5         	EventName$ MouseDoubleClick��6!   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6,   �� *
ShowPost+   CommandParameter 6I   ��  �� ElementName$ listView�� �� Path$ SelectedItem�5      ��5       �� 5!      .R�5"      �� 
���   �$��3q�61   $	��720��6E   ._�5#      ]� $��0,14,0,0q�6$   .+�5$      �  � RowDefinitions 5%   "   �� 5&   &   $
��Auto �64   �� 5'   &   $
��Auto �64   �� 5(   &   $
��Auto �64   .+�5*   "   �� $
��auto��6,    �� TextAlignment$
 Left=�6:   $
��Left=�6O   $��1��6j   $
��0,10q�6w   �� 5+   &   �6*   �� *Title+$ PropertyChanged=�5,   #   �� 5-   "   $��3��6,   $
��0,10q�69   �� 5.   &   $��12(�6p   $
��Bold"�6~   $��Italic$�6�   ���#007FFF�6*   �� *Author.FullName+$ PropertyChanged=�6�    �� 5/   &   $��10(�6j   $��DemiBold"�6x   �6*   �� *	Timestamp+$ PropertyChanged=�6x    �� 51   &   $�(��6*   $��10(�63   $��DemiBold"�6A    �� 52   &   $��10(�6E   $��DemiBold"�6S   �6*   �� *	NbAnswers+6S    �� 53   &      ListPosts_nbAnswers#�Z 6*   $��DemiBold"�6\   $��10(�6r    �� 54   &   $��10(�6A   $��DemiBold"�6O   �6*   �� *Score+6O    �� 55   &      ListPosts_score#�Z 6*   $��DemiBold"�6X   $��10(�6n    �� 56   &   $ 	listView3��6/   
������ $ Single=�6z   $��2��6�   $
��Auto��6�   ���#E0F8F7��6\   �� *Tags+��57   *   �� 58   .   .R�59   2   � 6>   5:   /   ��5<   *   �� 5=   .   .R�5>   2   �� �� 5?   6    �� Command 6@   �� *AffichePostsTag+$
 root�� �� CommandParameter 6v   �� .�6�   �� $��DemiBold"�6�   $��12(�6�   �6�   �� *	TagName+6�   6�   5@   3   5A   3   5C   '   5D   #   5E      5F      5G      5H      5J      5K      5L      