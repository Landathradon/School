   M S B A M L   `   `   `  ����I  EMultiLocations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:MultiLocationsMultiLocations  5   	   "    MultiLocations.AjoutPaiement   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   .localclr-namespace:MultiLocations     5   	     � Title$  AjoutPaiement��5   	   $��278.647��6   $��375.622��60    � 
ResizeMode$ NoResize=�6@   .��5	      � .+�5
   
   �� $��Effectuer Paiement��6   $��66,17,107,0q�6-   $	��Top=�6B   $��20(�6Z   $
��Bold"�6h   �� 5   
   $��ID Location��6   $
��Left=�6&   $��	10,83,0,0q�6A   $	��Top=�6T   �� 5   
   $��Montant��6   $
��Left=�6"   $��
10,166,0,0q�6=   $	��Top=�6Q   �� 5   
   $��Date Paiement��6   $
��Left=�6(   $��
186,83,0,0q�6C   $	��Top=�6W   �� 5   
   -   5         ��Name$ IDLocationList��5      $
��Left=�6+   $��24��6F   $��
10,114,0,0q�6R   $	��Top=�6f   $	��116��6~   $ �� IsSynchronizedWithCurrentItem$
 TrueY�6�    �� DisplayMemberPath$ 
NoLocation��6�   ��6�   �� 6�   �� 5   
   -   5         ��Name$ 
MontantTxt��5      $
��Left=�6&   $��23��6A   $��
10,197,0,0q�6M    �� TextWrapping$
 Wrap=�6a   $	��Top=�6u   $	��116��6�   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ DatePaiementDP��5      $
��Left=�6-   $��186,114,0,0q�6H   $	��Top=�6]   $	��140��6u   �� 5   
   -   5         ��Name$ btnPayer��5      $��Payer��6#   $
��Left=�63   $��186,200,0,0q�6N   $	��Top=�6c   $��75��6{   6�   5      5      