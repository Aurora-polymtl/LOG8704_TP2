# JeuTri

## Description du projet
Ce projet est une application mobile en réalité augmentée (AR) développée avec Unity 6000.0.54f1.  
Le joueur peut interagir avec des déchets virtuels dans son environnement réel, les ramasser avec les mains ou via un curseur tactile/souris, et les déposer dans la bonne poubelle.  
L’application inclut des messages éducatifs et un système de score pour sensibiliser au tri des déchets.

---

## Scène à build
La scène principale à utiliser pour tester le projet est :  
- **Menu** : depuis cette scène, le joueur peut démarrer une partie et accéder aux différentes fonctionnalités du jeu.

---

## Appareils et environnements de test
Le projet a été testé sur :  
- **Ordinateurs portables Windows** : utilisation du simulateur AR fourni par Unity.  
- **Téléphone Android** : tests en conditions réelles d’AR.

---

## Instructions pour tester
1. Ouvrir Unity et charger le projet.  
2. Vérifier que la scène **Menu** est incluse dans les **Build Settings**.  
3. Pour le simulateur :
   - Cliquer sur Play dans l’éditeur Unity.  
   - Interagir avec les déchets via la souris.  
4. Pour un appareil Android :
   - Connecter le téléphone à l’ordinateur.  
   - Configurer les **Player Settings** pour Android.  
   - Compiler et déployer l’application sur le téléphone.  
   - Interagir avec les déchets via l’écran tactile.  

---

## Informations utiles
- Le projet utilise **AR Foundation** pour la détection des surfaces et la position des objets dans le monde réel.  
- Les déchets sont générés uniquement sur les surfaces visibles par la caméra.  
- Les interactions peuvent se faire à la souris sur PC ou au toucher sur mobile.
- Dans PlayerSettings, le input doit être réglé à "New". 
