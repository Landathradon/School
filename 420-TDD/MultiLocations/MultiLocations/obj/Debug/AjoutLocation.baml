   M S B A M L   `   `   `  ����I  EMultiLocations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:MultiLocationsMultiLocations  5   	   "    MultiLocations.AjoutLocation   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   .localclr-namespace:MultiLocations     5   	     � Title$  AjoutLocation��5   	   $��390.747��6   $	��800��60    � 
ResizeMode$ NoResize=�6<   .��5	      � .+�5
   
   �� $��Ajouter Location��6   $��40��6+   $��309,37,289,0q�67   $	��Top=�6M   $��20(�6e   $
��Bold"�6s   �� 5   
   $��Date Début
��6   $
��Left=�6/   $��
62,143,0,0q�6J   $	��Top=�6^   $��24��6v   �� 5   
   $��Premier Paiement��6   $
��Left=�6+   $��
62,190,0,0q�6F   $	��Top=�6Z   �� 5   
   $��Montant��6   $
��Left=�6"   $��
62,237,0,0q�6=   $	��Top=�6Q   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ DateDebutDP��5      $
��Left=�6*   $��180,143,0,0q�6E   $	��Top=�6Z   $	��128��6r     5   
   -   5        $ PremierPaiementDP��5      $
��Left=�60   $��180,191,0,0q�6K   $	��Top=�6`   $	��128��6x   �� 5   
   -   5         ��Name$ 
MontantTxt��5      $
��Left=�6&   $��23��6A   $��180,241,0,0q�6M    �� TextWrapping$
 Wrap=�6b   $	��Top=�6v   $	��128��6�   �� 5   
   $��Terme��6   $
��Left=�6    $��453,143,0,0q�6;   $	��Top=�6P   �� 5   
   $	��Niv��6   $
��Left=�6   $��453,174,0,0q�69   $	��Top=�6N   �� 5   
   $��Client
��6   $
��Left=�6+   $��453,205,0,0q�6F   $	��Top=�6[   $��26��6s   �� 5   
   $��Nombre paiement��6   $
��Left=�6*   $��388,238,0,0q�6E   $	��Top=�6Z   �� 5   
   -   5         ��Name$ 	TermeList��5      $
��Left=�6&   $��22��6A   $��572,145,0,0q�6M   $	��Top=�6b   $	��180��6z   $ �� IsSynchronizedWithCurrentItem$
 TrueY�6�   ��6�   �� �� 5   
   -   5        $ NivList��5      $
��Left=�6$   $��26��6?   $��572,174,0,0q�6K   $	��Top=�6`   $	��180��6x   $
 TrueY�6�    �� DisplayMemberPath$ VINVehicule��6�   ��6�   �� 6�   �� 5   
   -   5        $ 
ClientList��5      $
��Left=�6'   $��26��6B   $��572,205,0,0q�6N   $	��Top=�6c   $	��180��6{   $
 TrueY�6�   $ 
NomComplet��6�   ��6�   �� 6�   �� 5   
   -   5        $ NbrPaiementList��5      $
��Left=�6,   $��23��6G   $��572,241,0,0q�6S   $	��Top=�6h   $	��180��6�   $
 TrueY�6�   ��6�   �� �� 5   
   -   5         ��Name$ 
BtnAjouter��5      $��Ajouter��6%   $��359,309,358,0q�67   $	��Top=�6N   6f   5      5      