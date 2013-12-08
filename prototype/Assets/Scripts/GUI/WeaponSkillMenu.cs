using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class WeaponSkillMenu : MonoBehaviour
{

		private bool menuIsOpen;
		private bool skillInfoIsOpen;
		private int indexOfChosenSkill;
		public GUISkin toolTipSkin;
		
		int WSx = 50;
		int WSy = 50;
		int WSWidth = 200;
	
		
		
		int SIWidth = 200;
		int XPadding = 10;
		// Use this for initialization
		void Start ()
		{
				WSx = Screen.width - XPadding * 2 - WSWidth - SIWidth;
		}
	
		void Update ()
		{
				if (Input.GetKeyDown ("n") && menuIsOpen) {
						closeMenu ();
				} else if (Input.GetKeyDown ("n") && !menuIsOpen) {
						openMenu ();
				}
		}
		
		void openMenu ()
		{
				Debug.Log ("Will open menu");
				menuIsOpen = true;
		}
		
		void closeMenu ()
		{
				Debug.Log ("Will close menu");
				menuIsOpen = false;
				skillInfoIsOpen = false;
		}
		
		void OnGUI ()
		{
				if (menuIsOpen) {
						drawWeaponSkillMenu ();
				}
				if (skillInfoIsOpen) {
						drawSkillInfo ();
				}
		}
		
		void drawWeaponSkillMenu ()
		{
		
				int WSHeight = 50;
				int WSYPadding = 20;
				//simply draw the same amount of boxes as we have lives.
				
				int currentY = WSy;
				ArrayList allSkills = Skill.allSkills;
				for (int skillIndex = 0; skillIndex < allSkills.Count; skillIndex++) {
						Skill aSkill = (Skill)allSkills [skillIndex];
						
						string weaponLevelInfo = "Level: " + aSkill.currentSkillLevel + " :: " + aSkill.chanceToAddEffect () * 100 + "%";
						string toPresent = SplitCamelCase (aSkill.componentName) + "\n" + SplitCamelCase (aSkill.currentWeaponType.ToString ());
						toPresent = toPresent + "\n" + weaponLevelInfo;
			
						if (GUI.Button (new Rect (WSx, currentY, WSWidth, WSHeight), toPresent, toolTipSkin.button)) {
								skillInfoIsOpen = true;
								indexOfChosenSkill = skillIndex;
						}
						currentY += WSHeight + WSYPadding;
						
				}
		}
		
		void drawSkillInfo ()
		{
				Skill skillToDraw = (Skill)Skill.allSkills [indexOfChosenSkill];
				int SIx = WSx + WSWidth + XPadding;
				int SIy = WSy;
				int SIHeight = 300;
				string toPresent = SplitCamelCase (skillToDraw.componentName) + "\n" + skillToDraw.description;
				GUI.Box (new Rect (SIx, SIy, SIWidth, SIHeight), toPresent, toolTipSkin.box);
		}
		
		string SplitCamelCase (string str)
		{
				return Regex.Replace (
		                     Regex.Replace (
		              str, 
		              @"(\P{Ll})(\P{Ll}\p{Ll})", 
		              "$1 $2" 
				), 
		                     @"(\p{Ll})(\P{Ll})", 
		                     "$1 $2" 
				);
		}
	
}
