   M S B A M L   `   `   `  ����K  GPRBD_2S_Aurélie, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:PRBD_2S_AuréliePRBD_2S_Aurélie  5      I EPRBD_Framework, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG4clr-namespace:PRBD_Framework;assembly=PRBD_FrameworkPRBD_Framework 5      &     PRBD_2S_Aurélie.AskQuestionView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:PRBD_2S_Aurélie     5      Bf4clr-namespace:PRBD_Framework;assembly=PRBD_Framework     5      -   5        $  PRBD_Framework.UserControlBase   Name$
  root��5      .��5
      � $��10q�6   ���#E0F8F7 � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5       � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      .+�5   
   �� $��Titre:��6   $
��Bold"�6!   $��1��63   $��Center=�6@   $��5q�6[   #  PRBD_Framework.ValidatedField  5   
   $��1��6   $��1��6(   $��Center=�68   .+�5      �� -   5         ��Name$ txtFullName��5       �� TextWrapping$
 Wrap=�6+   $��1��6?   ���#E0F8F7��5      �� *Title+ �� UpdateSourceTrigger$ PropertyChanged=�6R   5      �� 5   
   $��Entrez votre question: ��6   $
��Bold"�62   $��2��6D   $	��Top=�6Q   $��5q�6i     5   
   $��2��6   $��1��6(   $��Bottom=�68   .+�5      �� -   5        $ txtBody��5      $
 Wrap=�6'   $��1��6;    �� MinLines$ 8��5   Q    �� MaxLines$ 20��6^   " �� VerticalScrollBarVisibility$
 Auto=�6l   ���#E0F8F7��6   �� *Body+$ PropertyChanged=�6�   5      �� 5    
   $��Tags: ��6   $
��Bold"�6!   $��3��63   $	��Top=�6@   $��5q�6X     5!   
   $��3��6   $��2��6(   $��Bottom=�68   .+�5"      �� -   5        	 ��Name$	 txtTag��5"      
������ 
 �� SelectionMode$
 Single=�6_   $��3��6v   $��2��6�   ���#E0F8F7��6A   �� *Tags+��5#      �� 5$      .R�5%      � 6&   5&      ��5(      �� 5)      .R�5*      �� $��10q�6�   ��6#   �� *Name+ d� 	IsChecked 6<   �� *	IsChecked+��6\   �� *CheckTagCommand+ �� ElementName$
 root����6�   �� 6�   5+      5-      5.      � 5/   
   $��4��6   $��Center=�6!   $��Bottom=�6>   $��1��6Y   $	��300��6i   $��2��6u   .+�50      �� -   5         ��Name$ 	btnCancel��50      $��Annuler��6(   $	��100��6V   $
��Bold"�6b   $	��5,1q�6t   ��6:   �� *	Annuler+6t   �� 51      -   5        $ btnSave��51      $��Valider��6&   $	��100��6T   $
��Bold"�6`   $��20,0,0,0q�6r   $	��5,1q�6�   ��68   �� *	Valider+6�   52      53      54      