using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Shop : MonoBehaviour {

	// General variables
	private static Gamemaster gm;
	private static SFX sfx;
	private int helpTextDisplayTime = 3;
	private int priceBall = 50;
	private int priceClub = 180;
	private int priceGun = 500;

	// Shop UI variables
	private Button buyBall;
	private Button buyClub;
	private Button buyGun;
	private SpriteRenderer ballSoldOut;
	private SpriteRenderer clubSoldOut;
	private SpriteRenderer gunSoldOut;
	private Text ballPriceText;
	private Text clubPriceText;
	private Text gunPriceText;
	private Text itemBought;
	private Text insufficientFunds;
	private Text itemOwned;
	private Text funds;

	// Get Gamemaster, Audiomaster, Shop elements, hide certain elements
	void Start () {
	
		// Get Gamemaster
		if (gm == null) {
			gm = GameObject.Find("Gamemaster").GetComponent<Gamemaster>();
		}

		// Get Audiomaster
		if (sfx == null) {
			sfx = GameObject.Find("Audiomaster").GetComponent<SFX>();
		}

		// Get Shop elements
		buyBall = GameObject.Find("Button: Buy Ball").GetComponent<Button>();
		buyClub = GameObject.Find("Button: Buy Club").GetComponent<Button>();
		buyGun = GameObject.Find("Button: Buy Gun").GetComponent<Button>();
		ballSoldOut = GameObject.Find("Image: Ball Sold out").GetComponent<SpriteRenderer>();
		clubSoldOut = GameObject.Find("Image: Club Sold out").GetComponent<SpriteRenderer>();
		gunSoldOut = GameObject.Find("Image: Gun Sold out").GetComponent<SpriteRenderer>();
		ballPriceText = GameObject.Find("Text: Ball Price").GetComponent<Text>();
		clubPriceText = GameObject.Find("Text: Club Price").GetComponent<Text>();
		gunPriceText = GameObject.Find("Text: Gun Price").GetComponent<Text>();
		itemBought = GameObject.Find("Text: Bought item").GetComponent<Text>();
		insufficientFunds = GameObject.Find("Text: Insufficient funds").GetComponent<Text>();
		itemOwned = GameObject.Find("Text: Item Owned").GetComponent<Text>();
		funds = GameObject.Find("Text: Funds").GetComponent<Text>();

		// If the ball is not owned, hide sold-out cross, else show it and change text
		if (gm.GetOwnedWeapons()[1] == null) {
			ballSoldOut.enabled = false;
		} else {
			ballPriceText.text = "Sold out";
		}

		// If the club is not owned, hide sold-out cross, else show it and change text
		if (gm.GetOwnedWeapons()[2] != "Club") {
			clubSoldOut.enabled = false;
		} else {
			clubPriceText.text = "Sold out";
		}

		// If the gun is not owned, hide sold-out cross, else show it and change text
		if (gm.GetOwnedWeapons()[3] != "Gun") {
			gunSoldOut.enabled = false;
		} else {
			gunPriceText.text = "Sold out";
		}

		// Hide info texts
		itemBought.enabled = false;
		insufficientFunds.enabled = false;
		itemOwned.enabled = false;
	}

	// Update money display
	private void FixedUpdate () {
		funds.text =  ("Money: " + gm.GetMoney().ToString());
	}

	// Method bound to Buy Ball button
	public void BuyBallButton () {

		// If player has enough money and doesn't own the ball yet...
		if (gm.GetOwnedWeapons()[1] != "Ball" &&
			gm.GetMoney() >= priceBall) {

			// Add ball to ownedWeapons, Deduct price from money, Play sound, Show soldOut cross
			gm.SetOwnedWeapons("Ball", 1);
			gm.SetMoney(priceBall * -1);
			sfx.PlaySound(sfx.itemBought);
			ballSoldOut.enabled = true;
			ballPriceText.text = "Sold out";
			StartCoroutine(ItemBought());
		
		// If player already owns the ball, let them know
		} else if (gm.GetOwnedWeapons()[1] == "Ball") {
			StartCoroutine(ItemOwned());
		
		// If player doesn't have enough money, let them know
		} else {
			StartCoroutine(InsuffiecientFunds());
		}
	}

	// Method bound to Buy Club button
	public void BuyClubButton () {

		// If player has enough money and doesn't own the club yet...
		if (gm.GetOwnedWeapons()[2] != "Club" &&
			gm.GetMoney() >= priceClub) {

			// Add club to ownedWeapons, Deduct price from money, Play sound, Show soldOut cross
			gm.SetOwnedWeapons("Club", 2);
			gm.SetMoney(priceClub * -1);
			sfx.PlaySound(sfx.itemBought);
			clubSoldOut.enabled = true;
			clubPriceText.text = "Sold out";
			StartCoroutine(ItemBought());

		// If player already owns the club, let them know
		} else if (gm.GetOwnedWeapons()[2] == "Club") {
			StartCoroutine(ItemOwned());

		// If player doesn't have enough money, let them know
		} else {
			StartCoroutine(InsuffiecientFunds());
		}
	}

	// Method bound to Buy Gun button
	public void BuyGunButton () {

		// If player has enough money and doesn't own the gun yet...
		if (gm.GetOwnedWeapons()[3] != "Gun" &&
			gm.GetMoney() >= priceGun) {

			// Add gun to ownedWeapons, Deduct price from money, Play sound, Show soldOut cross
			gm.SetOwnedWeapons("Gun", 3);
			gm.SetMoney(priceGun * -1);
			sfx.PlaySound(sfx.itemBought);
			gunSoldOut.enabled = true;
			gunPriceText.text = "Sold out";
			StartCoroutine(ItemBought());

		// If player already owns the gun, let them know
		} else if (gm.GetOwnedWeapons()[3] == "Gun") {
			StartCoroutine(ItemOwned());

		// If player doesn't have enough money, let them know
		} else {
			StartCoroutine(InsuffiecientFunds());
		}
	}

	// Method hides other help texts, briefly shows itemBought help text 
	private IEnumerator ItemBought () {
		insufficientFunds.enabled = false;
		itemOwned.enabled = false;
		itemBought.enabled = true;
		yield return new WaitForSeconds(helpTextDisplayTime);
		itemBought.enabled = false;
	}

	// Method hides other help texts, briefly shows insufficientFunds help text
	private IEnumerator InsuffiecientFunds () {
		itemOwned.enabled = false;
		itemBought.enabled = false;
		insufficientFunds.enabled = true;
		yield return new WaitForSeconds(helpTextDisplayTime);
		insufficientFunds.enabled = false;
	}

	// Method hides other help texts, briefly shows itemOwned help text
	private IEnumerator ItemOwned () {
		itemBought.enabled = false;
		insufficientFunds.enabled = false;
		itemOwned.enabled = true;
		yield return new WaitForSeconds(helpTextDisplayTime);
		itemOwned.enabled = false;
	}
}
