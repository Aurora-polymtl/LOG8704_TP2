# LOG8704_TP2
# 🧩 Application de Réalité Augmentée (AR)

## 🎯 Objectif  
Développer une **application mobile de réalité augmentée (AR)** en utilisant les technologies de base d’AR Foundation, afin d’explorer la détection d’images, le placement d’objets 3D, les interactions simples et le suivi des mains.

---

## 🧠 Fonctionnalités principales

### 🖼️ Reconnaissance d’images  
- Utilisation de la bibliothèque **AR Foundation** et du composant **AR Tracked Image Manager**.  
- Détection d’une image spécifique (*tennisball3*) servant de marqueur.  
- Association de l’image reconnue à un objet 3D correspondant via le script **Prefab Image Pair Manager**.

---

### 🧱 Placement d’objets 3D  
- Instanciation d’un **cube** dans l’espace AR au moment de la détection.  
- Positionnement dynamique en fonction de l’image reconnue.  

---

### 🌀 Interactions simples  
- Détection des **taps** sur l’écran.  
- Déclenchement d’une **animation de rotation** du cube lors d’un tap.

---

### 🧭 Interface utilisateur  
- Création d’une interface avec un **Canvas** nommé *Popup*.  
- Affichage d’informations ou de contrôles liés à l’expérience AR.

---

### ✋ Suivi des mains  
- Intégration de **MediaPipe** pour la détection et le suivi des mains.  
- L’application détecte lorsqu’une main se **ferme**, permettant d’envisager des interactions gestuelles futures.

---

## ⚙️ Technologies utilisées  
- **Unity** 6000.0.54f1  
- **AR Foundation**  
- **ARCore / XR Plug-in Management**  
- **MediaPipe (plugin Unity)**  
- **C# (scripts Unity)**  

---

## 📱 Plateforme cible  
- **Android**  
- Tests effectués sur téléphone Android et dans le simulateur Unity.  
- Environnement de test : **intérieur**, avec un éclairage standard.

---

## 🚧 Limitations et problèmes connus  
- Le suivi des mains via MediaPipe peut varier selon les conditions d’éclairage.  
- La détection d’image est limitée à **une seule image** suivie simultanément.  
- Certains composants AR nécessitent des **permissions Android** spécifiques.

---

## 👥 Équipe de développement  
Projet réalisé dans le cadre d’un travail de groupe universitaire sur la Réalité Augmentée.  
