   M S B A M L   `   `   `  ����I  ETeam5Projet2CP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Team5Projet2CPTeam5Projet2CP  5          Team5Projet2CP.Crayon   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .localclr-namespace:Team5Projet2CP     5        N� Title$  Crayon��5	       N� 
Background
 �����.��5      � .+�5   
   �  � RowDefinitions 5      �� 5      $��70 �6    �� 5      $	��850 �6     � ColumnDefinitions 5      �� 5      $	��250 �6#   �� 5      $
��1000 �6#   .+�5      �� $��0��6   $��0��5      $��Bottom=�5      $
��Left=�5      $��17(�5      $
��Bold"�6"   $��80 80 0 822q�5       $��2��5!      
Shartape5#      �� 5$      $��0��6   $��2��6&   $��0��67   .+�5%      �� $��logo.png��6   $
��Left=�5&      $��	50 70 0 0q�5'      $��40��6'   $��40��63   6@   5(      �� 5,      $��20,120,20,0q�6   ���#b3b3b3$	��193��5-   #   $��0q�5.      
������� �� CornerRadius$ 10��5/      $��1��6   $��0��6,   $	��Top=�50       J� Effect 51      3  -System.Windows.Media.Effects.DropShadowEffect  52         	Direction$	 -75Q�6'      ShadowDepth$ 2Q�67      Opacity$	 0.1Q�6G   	   
BlurRadius$	 25Q�6U   
   Color$
 Black��6e   ._�54      �� -   5        $��0��54      $��2��6)   $��50��55      $��0 35.155 0 0q�56      $	��Top=�6%   
������ 
������ $	��210��59   @   .��5:      �� $��2��6    $��/Fonts/#Poppins)�5;      $��0��69   $��Center=�6I   $��16(�6e   $��Light"�6s   $
��Left=�5<      $��	-75 0 0 0q�6.   	Accueil5>      5?      5A      �� 5C      -   5        $��0��5C      $��2��6%   $��50��5D      $��	0 220 0 0q�5E      $	��Top=�6"   
������ 
������ $	��210��5H   @   .��5I      �� $��2��6   $��/Fonts/#Poppins)�5J      $��0��69   $��Center=�6I   $��16(�6e   $��Light"�6s   $
��Left=�5K      $��	-75 0 0 0q�6.   	à propos5M      5N      �� 5Q      $��20, 350,20,435q�6   ���#b3b3b3$��0q�5S      
�������$ 10��5T      $��3��6   $��3��5U      $��0��6"    5W        5X      $	 -75Q�6'   $ 2Q�5Y   #   $	 0.1Q�5Z   #   $	 25Q�61   $
 Black��6A   ._�5]      �� $��3��6   $��0��6)   $	��Top=�69   $
��Left=�6Q   $��16(�6l   $��Light"�6z   $��	35 25 0 0q�5^      
���   �$��/Fonts/#Poppins)�5`       � 5a      ��5c      �� 5d      �� 5e      $!��mailto:Nao2001.hl@gmail.com?�6   .�6I   Contactez-nous5f   (   64   5j      5l      5m      �� 5p      $��0��6   $��1��5q      $��Bottom=�5r      $��Right=�5s      $��17(�5t      $��Light"�6&   $��
6,0,865,10q�5u      
�������$��/Fonts/#Poppins)�5w      Centre d'aide5z      �� 5|      $��0��6   $��1��6    	 �� X1$ 45��5}   
   	 �� Y1$ 70��6   	 �� X2$	 920��5~   
   	 �� Y2$ 70��6   ���#d3d3d3$	��0.8��5�   
   �� 5�      $��Hidden=�6   $	��Top=�5�      $��1��60   $��2��6@   $��1��5�      $��	0 0 -10 0q�6$   $��Right=�67   $
��1000��6S   $	��590��6`   .��5�      �  5�      �� 5�      $��50 �6(   �� 5�      $	��800 �6(   �� 5�      $��70 �6(   .+�5�      �� $��0��6    $
��Left=�6-   $��Center=�6H   $��/Fonts/#Poppins)�5�       $��28(�6=   $
��Bold"�6K   $��	33 20 0 0q�6]   Crayon5�      �� 5�      $��1��6   $��
35 20 30 0q�6*   $	��0.7q�6>   $
��Left=�6T   ���#DADADA$ 10��5�      $	��900��6/   ._�5�      �� $��/Fonts/#Poppins)�6$   $��16(�6A   $��Light"�6O   $��	20 15 0 0q�6b   
L'outil �� 5�   &   .�6-   Crayon65    de �� 6A   .�6H   
Shartape6R   IG permet de dessiner des polygones manuellement en connctant ses traits.�� 6�   �� 6�   �� 5�      $��17(�6(   $��Light"�66   �� 6J   .�6Q   Comment utilser le crayon?6m   6v   �� 5�      �� 6*   HF1. Séléctionnez l'outil Crayon qui apparait dans la barre des outils�� 5�      B@2. Dans l'espace du travail, cliquez pour commencer votre dessin�� 5�      nl3. Vous allez commencer à construire votre premier trait du polygone. Controlez sa direction et logngeur en�� 5�      ultilisant votre souris.�� 5�      MK4. Cliquez sur le point que vous voulez être le point final de cette ligne�� 5�      B@5. Commnecez à dessiner le deuxieme trait à partir de ce point�� 5�      536. Double-cliquez pour construire le polygone final�� 6_   �� 5�      $��50 30 30 30q�6)   .+�5�   "   �� $��Screenshots/crayon.png��6(   $	��400��6H    5�   &     5�   '   $
 Black��68   $	 -75Q�6F   $	 2.5Q�5�   '   $
 0.15Q�69   $	 15Q�6H   5�   #   5�      �� 5�      CAVous pouvez par la suite modifier les propriétés de cette forme5�      5�      �� 5�      $��2��6    $��/Fonts/#Poppins)�6-   
���   �$
��Left=�5�      $��Center=�5�      $��14(�5�      $��Light"�6*   $��60 0 0 0q�5�      -+Vous ne pouvez pas trouver votre réponse? �� 5�   @   $!��mailto:Nao2001.hl@gmail.com?�6K   .�6u   Contactez-nous5�   (   5�      5�      5�      ]� 5�      $��0��6   $��2��6&   $��0��67   $��5��5�      .+�5�       � -   5          �Name$ _mainFrame5��5�        � NavigationUIVisibility$ Hidden=�6-   6M   5�      5�      5�      5�      