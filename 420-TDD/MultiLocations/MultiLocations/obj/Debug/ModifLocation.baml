   M S B A M L   `   `   `  ����I  EMultiLocations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:MultiLocationsMultiLocations  5   	   "    MultiLocations.ModifLocation   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   .localclr-namespace:MultiLocations     5   	     � Title$  ModifLocation��5   	   $��368.327��6   $	��800��60    � 
ResizeMode$ NoResize=�6<   .��5	      � .+�5
   
   �� $��Modifier Location��6   $��307,37,295,0q�6,   $	��Top=�6B   $
��Bold"�6Z   $��20(�6l   �� 5   
   $��Date Début
��6   $
��Left=�6*   $��
57,146,0,0q�6E   $	��Top=�6Y   $��24��6q   �� 5   
   $��Premier Paiement��6   $
��Left=�6+   $��
57,177,0,0q�6F   $	��Top=�6Z   �� 5   
   $��Montant��6   $
��Left=�6"   $��
57,210,0,0q�6=   $	��Top=�6Q   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ DateDebutDP��5      $
��Left=�6*   $��170,146,0,0q�6E   $	��Top=�6Z   $	��133��6r     5   
   -   5        $ PremierPaiementDP��5      $
��Left=�60   $��170,179,0,0q�6K   $	��Top=�6`   $	��133��6x   �� 5   
   -   5         ��Name$ 
MontantTxt��5      $
��Left=�6&   $��23��6A   $��170,210,0,0q�6M    �� TextWrapping$
 Wrap=�6b   $	��Top=�6v   $	��133��6�   �� 5   
   $��Terme��6   $
��Left=�6    $��448,115,0,0q�6;   $	��Top=�6P   �� 5   
   $	��Niv��6   $
��Left=�6   $��448,146,0,0q�69   $	��Top=�6N   �� 5   
   $��Client
��6   $
��Left=�6&   $��448,177,0,0q�6A   $	��Top=�6V   $��26��6n   �� 5   
   $��Nombre paiement��6   $
��Left=�6*   $��383,210,0,0q�6E   $	��Top=�6Z   �� 5   
   -   5         ��Name$ 	TermeList��5      $
��Left=�6&   $��22��6A   $��567,117,0,0q�6M   $	��Top=�6b   $	��180��6z   $ �� IsSynchronizedWithCurrentItem$
 TrueY�6�   ��6�   �� �� 5   
   -   5        $ NivList��5      $
��Left=�6$   $��26��6?   $��567,146,0,0q�6K   $	��Top=�6`   $	��180��6x   $
 TrueY�6�    �� DisplayMemberPath$ VINVehicule��6�   ��6�   �� 6�   �� 5   
   -   5        $ 
ClientList��5      $
��Left=�6'   $��26��6B   $��567,177,0,0q�6N   $	��Top=�6c   $	��180��6{   $
 TrueY�6�   $ 
NomComplet��6�   ��6�   �� 6�   �� 5   
   -   5        $ NbrPaiementList��5      $
��Left=�6,   $��23��6G   $��567,213,0,0q�6S   $	��Top=�6h   $	��180��6�   $
 TrueY�6�   ��6�   �� �� 5   
   -   5         ��Name$ BtnModifier��5      $��Modifier��6&   $��354,281,363,0q�69   $	��Top=�6P   6h   �� 5   
   $��ID Location��6   $
��Left=�6&   $��
58,115,0,0q�6A   $	��Top=�6U   �� 5   
   -	   5        $ IDLocationList��5      $
��Left=�6+   $��22��6F   $��170,117,0,0q�6R   $	��Top=�6g   $	��133��6   $
 TrueY�6�   $ 
NoLocation��6�   ��6�   �� 6�   5      5      