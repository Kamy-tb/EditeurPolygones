   M S B A M L   `   `   `  ����I  ETeam5Projet2CP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Team5Projet2CPTeam5Projet2CP  5      +    %Team5Projet2CP.OpérationsBooléennes   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .localclr-namespace:Team5Projet2CP     5        N� Title$  OpérationsBooléennes��5	       N� 
Background
 �����.��5      �  � RowDefinitions 5   
   �� 5      $��70 �6   �� 5      $	��850 �6    � ColumnDefinitions 5   
   �� 5      $	��250 �6   �� 5      $
��1000 �6   .+�5   
   �� $��0��6   $��0��5      $��Bottom=�5      $
��Left=�5      $��17(�5       $
��Bold"�6"   $��80 80 0 822q�5!      $��2��5"      
Shartape5$      �� 5%   
   $��0��6   $��2��6"   $��0��63   .+�5&      �� $��logo.png��6   $
��Left=�5'      $��	50 70 0 0q�5(      $��40��6'   $��40��63   6@   5)      �� 5,   
   $��20,120,20,0q�6   ���#b3b3b3$	��193��5-   #   $��0q�5.      
������� �� CornerRadius$ 10��5/      $��1��6   $��0��6,   $	��Top=�50       J� Effect 51      3  -System.Windows.Media.Effects.DropShadowEffect  52         	Direction$	 -75Q�6#      ShadowDepth$ 2Q�63      Opacity$	 0.1Q�6C   	   
BlurRadius$	 25Q�6Q   
   Color$
 Black��6a   ._�54      �� -   5        $��0��54      $��2��6%   $��50��55      $��0 35.155 0 0q�56      $	��Top=�6%   
������ 
������ $	��210��59   @   .��5:      �� $��2��6   $��/Fonts/#Poppins)�5;      $��0��69   $��Center=�6I   $��16(�6e   $��Light"�6s   $
��Left=�5<      $��	-75 0 0 0q�6.   	Accueil5>      5?      5A      �� 5C   
   -   5        $��0��5C      $��2��6!   $��50��5D      $��	0 220 0 0q�5E      $	��Top=�6"   
������ 
������ $	��210��5H   @   .��5I      �� $��2��6   $��/Fonts/#Poppins)�5J      $��0��69   $��Center=�6I   $��16(�6e   $��Light"�6s   $
��Left=�5K      $��	-75 0 0 0q�6.   	à propos5M      5N      �� 5Q   
   $��20, 350,20,435q�6   ���#b3b3b3$��0q�5S      
�������$ 10��5T      $��3��6   $��3��5U      $��0��6"    5W        5X      $	 -75Q�6#   $ 2Q�5Y   #   $	 0.1Q�5Z   #   $	 25Q�61   $
 Black��6A   ._�5]      �� $��3��6   $��0��6%   $	��Top=�65   $
��Left=�6M   $��16(�6h   $��Light"�6v   $��	35 25 0 0q�5^      
���   �$��/Fonts/#Poppins)�5`       � 5a      ��5c      �� 5d      �� 5e      $!��mailto:Nao2001.hl@gmail.com?�6   .�6I   Contactez-nous5f   (   64   5j      5l      5m      �� 5p   
   $��0��6   $��1��5q      $��Bottom=�5r      $��Right=�5s      $��17(�5t      $��Light"�6&   $��
6,0,835,10q�5u      
�������$��/Fonts/#Poppins)�5w      Centre d'aide5z      �� 5|   
   $��0��6   $��1��6   	 �� X1$ 70��5}   
   	 �� Y1$ 70��6   	 �� X2$	 920��5~   
   	 �� Y2$ 70��6   ���#d3d3d3$	��0.8��5�   
   �� 5�   
   $��Hidden=�6   $	��560��5�      $	��Top=�5�      $��1��60   $��1��5�      $��	0,30,-7,0q�6$   $	��635��67   .��5�      � $��Right=�6    5�      �� 5�      $��50 �6$   �� 5�      $
��1010 �6$   �� 5�      $��70 �6$   .+�5�      �� $��0��6   $��Center=�5�      $��Right=�5�      $��28(�5�      $��Light"�6&   $��-40,0,640,16q�5�      $��/Fonts/#Poppins)�5�      �� 5�      .�6   Combinez des formes!6-   5�      �� 5�      $��-50,10,0,10q�6   $	��0.7q�5�      $
��Left=�6'   ���#DADADA$��1��5�   '   $ 10��5�      ._�5�      �� 
���   �$	��Top=�5�      $
��Left=�5�      $��/Fonts/#Poppins)�5�      $��20.333,20.333,0,0q�5�      $��Light"�5�      $
��1135��6/   $��14(�5�      $	��833��6*   �� 5�      $��/Fonts/#Poppins)�6    $��Regular"�6=   $��16(�6R   Créez des polygones6v   �� 5�      �� 6"   �� 6.   .�65   
Shartape6?   zx permet de créer des formes simples avec des caractéristiques particulières, mais aussi des formes complexes avec ces�� 6�   75mêmes propriétés. Pour cela, procédez comme suit:�� 6�   ~|1. La première étape consiste à sélectionner les deux formes simples / complexes que vous souhaitez combiner. Pour cela,�� 5�   �   /-commencez par cliquer sur la première forme.�� 5�   F   ��2. Cliquez ensuite sur l’outil ‘select’. Une boite de dialogue apparaîtra pour confirmer que celui a été bien séléctionné�� 5�      �� 5�      $��	20 30 0 0q�6%   .+�5�      �� $ ��/ScreenShots/SelectItm.png��6   $
��Left=�6@   $	��360��6[    5�        5�      $
 Black��6/   $	 -75Q�6=   $	 2.5Q�5�   #   $
 0.15Q�65   $	 15Q�6D   5�      5�       �� 5�      $��	50 30 0 0q�6%   .+�5�      �� $%��/ScreenShots/elementAjouté.png��6   $
��Left=�6D   $	��190��6_    5�        5�      $
 Black��6/   $	 -75Q�6=   $	 2.5Q�5�   #   $
 0.15Q�65   $	 15Q�6D   5�      5�      �� 5�      �� 6   �� 6   863. Refaites les mêmes étapes pour la deuxième forme�� 5�   O   QO4. Choisissez par la suite l’opération booléennes que vous voulez appliquer�� 5�   f   �� 5�      $��
15 25 0 10q�6%   .+�5�      �� $(��"/ScreenShots/Union_Soust_inter.png��6   $
��Left=�6H   $	��800��6c    5�        5�      $
 Black��6/   $	 -75Q�6=   $	 2.5Q�5�   #   $
 0.15Q�65   $	 15Q�6D   5�      5�      5�      5�      �� 5�      $��2��6   $��/Fonts/#Poppins)�6)   
���   �$
��Left=�5�      $��Center=�5�      $��14(�5�      $��Light"�6*   $��60 0 0 0q�5�      -+Vous ne pouvez pas trouver votre réponse? �� 5�   @   $!��mailto:Nao2001.hl@gmail.com?�6K   .�6u   Contactez-nous5�   (   5�      5�      5�      ]� 5�   
   $��0��6   $��2��6"   $��0��63   $��5��5�      .+�5�       � -   5          �Name$ _mainFrame3��5�        � NavigationUIVisibility$ Hidden=�6)   6I   5�      5�      5�      