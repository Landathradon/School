   M S B A M L   `   `   `  ����J  FLINQ_Exercice_1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:LINQ_Exercice_1LINQ_Exercice_1  5   	        LINQ_Exercice_1.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:LINQ_Exercice_1     5   	     � Title$  Exercice LINQ��5   	   $	��350��6   $	��525��6,    � 
ResizeMode$ NoResize=�68    � WindowStartupLocation$ CenterScreen=�6N   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $��* �6   �� 5      $��4* �6   �� 5      $��* �6   �� 5      $��4* �6   �� 5      $��* �6   �� 5      $��4* �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��5* �6   �� 5      $��* �6   �� 5      $��2* �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��1��6   $��5��6-   $��Center=�6A   $��Times New Roman)�6^   $��16(�6{   $
��Bold"�6�   $��Italic$�6�   .��6�   Liste de nombres6�   �� 5   
   $��1��6   $��1��6   $��Center=�6-   $��Bottom=�6J   $��Times New Roman)�6e   $��14(�6�   $
��Bold"�6�   $��Italic$�6�   .��6�   Liste complète6�   �� 5   
   $��1��6   $��3��6   $��Center=�6-   $��Bottom=�6J   $��Times New Roman)�6e   $��14(�6�   $
��Bold"�6�   $��Italic$�6�   .��6�   Nombres pairs6�   �� 5   
   $��1��6   $��5��6   $��Center=�6-   $��Bottom=�6J   $��Times New Roman)�6e   $��14(�6�   $
��Bold"�6�   $��Italic$�6�   .��6�   Nombres impairs6�   �� 5   
   -   5         ��Name$ Complete��5      $��1��6$   $��2��64   $��Times New Roman)�6A   �� 5    
   -   5        $
 Pair��5       $��3��6    $��2��60   $��Times New Roman)�6=   �� 5!   
   -   5        $ Impair��5!      $��5��6"   $��2��62   $��Times New Roman)�6?   �� 5"   
   -   5         ��Name$ AfficherListe��5"      $��1��6(   $��4��68   $��Times New Roman)�6E   $��Liste complète��6b   $
��Bold"�6{   $��Italic$�6�   6�   �� 5#   
   -   5        $ AfficherPair��5#      $��3��6'   $��4��67   $��Times New Roman)�6D   $��Nombres pairs��6a   $
��Bold"�6y   $��Italic$�6�   6�   �� 5$   
   -   5        $ AfficherImpair��5$      $��5��6)   $��4��69   $��Times New Roman)�6F   $��Nombres impairs��6c   $
��Bold"�6}   $��Italic$�6�   6�   5%      5&      