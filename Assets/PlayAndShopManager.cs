using Solana.Unity.Metaplex.NFT.Library;
using Solana.Unity.Metaplex.Utilities;
using Solana.Unity.Programs;
using Solana.Unity.Rpc.Builders;
using Solana.Unity.Rpc.Core.Http;
using Solana.Unity.Rpc.Models;
using Solana.Unity.Rpc.Types;
using Solana.Unity.SDK;
using Solana.Unity.SDK.Nft;
using Solana.Unity.Wallet;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayAndShopManager : MonoBehaviour
{

    // Item Buttons
    public Button buttonHeart;

    // NFT Buttons
    public Button buttonPiggy;
    public Button buttonDoggy;
    public Button buttonGoaty;
    public Button buttonTurtle;
    public Button buttonKoala;
    public Button buttonSheep;
    public Button buttonDuck;

    public Text buttonPiggyText;
    public Text buttonDoggyText;
    public Text buttonGoatyText;
    public Text buttonTurtleText;
    public Text buttonKoalaText;
    public Text buttonSheepText;
    public Text buttonDuckText;


    public Button backBtn;

    public TextMeshProUGUI buyingStatusText;

    private const long SolLamports = 1000000000;

    private void Start()
    {
        CheckFirstLogin();        
    }

    void UpdateButtonStates()
    {
        if (PlayerPrefs.GetInt("PiggyCount", 0) == 1)
        {
            buttonPiggyText.text = "Owned";
        }
        if (PlayerPrefs.GetInt("DoggyCount", 0) == 1)
        {
            buttonDoggyText.text = "Owned";
        }
        if (PlayerPrefs.GetInt("GoatyCount", 0) == 1)
        {
            buttonGoatyText.text = "Owned";
        }
        if (PlayerPrefs.GetInt("TurtleCount", 0) == 1)
        {
            buttonTurtleText.text = "Owned";
        }
        if (PlayerPrefs.GetInt("KoalaCount", 0) == 1)
        {
            buttonKoalaText.text = "Owned";
        }
        if (PlayerPrefs.GetInt("SheepCount", 0) == 1)
        {
            buttonSheepText.text = "Owned";
        }
        if (PlayerPrefs.GetInt("DuckCount", 0) == 1)
        {
            buttonDuckText.text = "Owned";
        }
    }

    void CheckFirstLogin()
    {
        // Kiểm tra xem đã lưu thông tin lần đầu đăng nhập chưa
        if (!PlayerPrefs.HasKey("isFirstLogin"))
        {
            // Nếu chưa lưu, đây là lần đầu đăng nhập
            Debug.Log("This is the first login!");

            // Thực hiện hành động khi lần đầu đăng nhập (tặng thưởng, hướng dẫn, v.v.)
            HandleFirstLogin();

            // Lưu trạng thái đã đăng nhập lần đầu
            PlayerPrefs.SetInt("isFirstLogin", 1);
            PlayerPrefs.Save();
        }
        else
        {
            // Nếu đã lưu, không phải lần đầu đăng nhập
            Debug.Log("Welcome back!");
            UpdateButtonStates();
        }
    }

    public async void HandleFirstLogin()
    {
        // Thực hiện các hành động dành cho lần đầu đăng nhập ở đây
        // Ví dụ: Thưởng cho người chơi, mở tutorial, v.v.
        Debug.Log("Handling first login actions...");

        // Check if Player already Owned the NFT
        var tokenAccounts = await Web3.Wallet.GetTokenAccounts(Commitment.Processed);
        foreach (var item in tokenAccounts)
        {
            var loadTask = Nft.TryGetNftData(item.Account.Data.Parsed.Info.Mint,
            Web3.Instance.WalletBase.ActiveRpcClient, commitment: Commitment.Processed);
            var nftData = await loadTask;
            if (nftData != null)
            {
                Debug.Log($"NFT Mint: {nftData}");
                string textValue = nftData.metaplexData?.data?.offchainData?.name;
                Debug.Log("textValue: " + textValue);
                if (textValue == "Piggy")
                {
                    PlayerPrefs.SetInt("PiggyCount", 1);
                    PlayerPrefs.Save();
                    ResourceBoost.Instance.piggy = 1;
                }
                else if (textValue == "Doggy") {
                    PlayerPrefs.SetInt("DoggyCount", 1);
                    PlayerPrefs.Save();
                    ResourceBoost.Instance.doggy = 1;
                }
                else if (textValue == "Goaty")
                {
                    PlayerPrefs.SetInt("GoatyCount", 1);
                    PlayerPrefs.Save();
                    ResourceBoost.Instance.goaty = 1;
                }
                else if (textValue == "Turtle")
                {
                    PlayerPrefs.SetInt("TurtleCount", 1);
                    PlayerPrefs.Save();
                    ResourceBoost.Instance.turtle = 1;
                }
                else if (textValue == "Koala")
                {
                    PlayerPrefs.SetInt("KoalaCount", 1);
                    PlayerPrefs.Save();
                    ResourceBoost.Instance.koala = 1;
                }
                else if (textValue == "Sheep")
                {
                    PlayerPrefs.SetInt("SheepCount", 1);
                    PlayerPrefs.Save();
                    ResourceBoost.Instance.sheep = 1;
                }
                else if (textValue == "Duck")
                {
                    PlayerPrefs.SetInt("DuckCount", 1);
                    PlayerPrefs.Save();
                    ResourceBoost.Instance.duck = 1;
                }
            }
            else
            {
                Debug.Log($"Can not find NFT Data: {item.Account.Data.Parsed.Info.Mint}");
            }
        }
        UpdateButtonStates();
    }


    public void PlayGame()
    {
        // Chuyển sang scene mới (ShopAndPlay)
        SceneManager.LoadScene("CS_StartMenu");
    }

    private void HandleResponse(RequestResult<string> result)
    {
        Debug.Log(result.Result == null ? result.Reason : "");
    }

    private void HideButtons()
    {
        buttonHeart.interactable = false;

        buttonPiggy.interactable = false;
        buttonDoggy.interactable = false;
        buttonGoaty.interactable = false;
        buttonTurtle.interactable = false;
        buttonKoala.interactable = false;
        buttonSheep.interactable = false;
        buttonDuck.interactable = false;

        backBtn.interactable = false;
    }

    private void ShowButtons()
    {
        buttonHeart.interactable = true;

        buttonPiggy.interactable = true;
        buttonDoggy.interactable = true;
        buttonGoaty.interactable = true;
        buttonTurtle.interactable = true;
        buttonKoala.interactable = true;
        buttonSheep.interactable = true;
        buttonDuck.interactable = true;

        backBtn.interactable = true;
    }

    public async void SpendTokenToBuyHeart(int indexValue)
    {

        Double _ownedSolAmount = await Web3.Instance.WalletBase.GetBalance();

        if (_ownedSolAmount <= 0)
        {
            buyingStatusText.text = "Not Enough SOL";
            return;
        }

        HideButtons();

        float costValue = 0.002f;
        buyingStatusText.text = "Buying...";
        buyingStatusText.gameObject.SetActive(true);       
        try
        {
            RequestResult<string> result = await Web3.Instance.WalletBase.Transfer(
               new PublicKey("Hw1VoYsnB7kX5h4nZiczEndj6mMF3i7DZR5Q2Ng1JiM4"),
               Convert.ToUInt64(costValue * SolLamports));
            HandleResponse(result);

            ShowButtons();

            buyingStatusText.text = "+1 Heart";

            buttonHeart.gameObject.SetActive(false);
            ResourceBoost.Instance.heartBoost += 1;

        }
        catch (Exception ex)
        {
            // Xử lý ngoại lệ nếu có lỗi xảy ra
            Debug.LogError($"Lỗi khi thực hiện chuyển tiền: {ex.Message}");
        }
    }

    public async void BuyGameNFT(int indexValue)
    {

        // Mint and ATA
        var mint = new Account();
        var associatedTokenAccount = AssociatedTokenAccountProgram
            .DeriveAssociatedTokenAccount(Web3.Account, mint.PublicKey);

        // Define the metadata
        // 7 NFTs here
        var metadata = new Metadata() { };

        if (indexValue == 0)
        {
            metadata = new Metadata()
            {
                name = "Piggy",
                symbol = "Piggy",

                // Deployed to Pinata
                uri = "https://gateway.pinata.cloud/ipfs/QmTEe5kw1bwhaWN9MGD5MCsdBM5GqRVd9peqfBRjszgR4m",
                sellerFeeBasisPoints = 0,
                creators = new List<Creator> { new(Web3.Account.PublicKey, 100, true) }
            };
        }
        else if (indexValue == 1)
        {
            metadata = new Metadata()
            {
                name = "Doggy",
                symbol = "Doggy",

                // Deployed to Pinata
                uri = "https://gateway.pinata.cloud/ipfs/QmNfZHTGagUENZpQwZ4zdokc8xZmuzekj8s9Ni7kk3gz4S",
                sellerFeeBasisPoints = 0,
                creators = new List<Creator> { new(Web3.Account.PublicKey, 100, true) }
            };
        }
        else if (indexValue == 2)
        {
            metadata = new Metadata()
            {
                name = "Goaty",
                symbol = "Goaty",

                // Deployed to Pinata
                uri = "https://gateway.pinata.cloud/ipfs/QmRUCnvx1CporMYswBzbfSQsNZ9FR9EBTFYAWSjQKz52g2",
                sellerFeeBasisPoints = 0,
                creators = new List<Creator> { new(Web3.Account.PublicKey, 100, true) }
            };
        }
        else if (indexValue == 3)
        {
            metadata = new Metadata()
            {
                name = "Turtle",
                symbol = "Turtle",

                // Deployed to Pinata
                uri = "https://gateway.pinata.cloud/ipfs/QmXUnMxqaSKyQAUQFEdNgp76WA6jT6VnJaWnf6p9YwGLgf",
                sellerFeeBasisPoints = 0,
                creators = new List<Creator> { new(Web3.Account.PublicKey, 100, true) }
            };
        }
        else if (indexValue == 4)
        {
            metadata = new Metadata()
            {
                name = "Koala",
                symbol = "Koala",

                // Deployed to Pinata
                uri = "https://gateway.pinata.cloud/ipfs/QmNtFVoekQsw57VdmuN7gwWgrzYpLDsTnL7GrkzzhbWK4g",
                sellerFeeBasisPoints = 0,
                creators = new List<Creator> { new(Web3.Account.PublicKey, 100, true) }
            };
        }
        else if (indexValue == 5)
        {
            metadata = new Metadata()
            {
                name = "Sheep",
                symbol = "Sheep",

                // Deployed to Pinata
                uri = "https://gateway.pinata.cloud/ipfs/QmUoifybVJxtHFqHijVoBjqKw4tc7RcaPUvvUs74muEJC7",
                sellerFeeBasisPoints = 0,
                creators = new List<Creator> { new(Web3.Account.PublicKey, 100, true) }
            };
        }
        else
        {
            metadata = new Metadata()
            {
                name = "Duck",
                symbol = "Duck",

                // Deployed to Pinata
                uri = "https://gateway.pinata.cloud/ipfs/QmZscdG4CmZAtNMu84zK4EwWVPc2H3vZCHp7z92Mt3hzKh",
                sellerFeeBasisPoints = 0,
                creators = new List<Creator> { new(Web3.Account.PublicKey, 100, true) }
            };
        }

        // Check if Player already Owned the NFT
        var tokenAccounts = await Web3.Wallet.GetTokenAccounts(Commitment.Processed);
        int matchingNftCount = 0;
        string checkTextValue = "";

        if (indexValue == 0)
        {
            checkTextValue = "Piggy";
        }
        else if (indexValue == 1)
        {
            checkTextValue = "Doggy";
        }
        else if (indexValue == 2)
        {
            checkTextValue = "Goaty";
        }
        else if (indexValue == 3)
        {
            checkTextValue = "Turtle";
        }
        else if (indexValue == 4)
        {
            checkTextValue = "Koala";
        }
        else if (indexValue == 5)
        {
            checkTextValue = "Sheep";
        }
        else if (indexValue == 6)
        {
            checkTextValue = "Duck";
        }

        foreach (var item in tokenAccounts)
        {
            var loadTask = Nft.TryGetNftData(item.Account.Data.Parsed.Info.Mint,
            Web3.Instance.WalletBase.ActiveRpcClient, commitment: Commitment.Processed);
            var nftData = await loadTask;
            if (nftData != null)
            {
                Debug.Log($"NFT Mint: {nftData}");
                string textValue = nftData.metaplexData?.data?.offchainData?.name;
                Debug.Log("textValue: " + textValue);
                if (textValue == checkTextValue)
                {
                    matchingNftCount += 1;
                    break;
                }
            }
            else
            {
                Debug.Log($"Can not find NFT Data: {item.Account.Data.Parsed.Info.Mint}");
            }
        }


        // If already have then notify the Player
        if (matchingNftCount >= 1)
        {
            buyingStatusText.text = "You already own this NFT.";
            buyingStatusText.gameObject.SetActive(true);

            ShowButtons();

            // Hide the NFT Buttons.
            if (indexValue == 0)
            {
                PlayerPrefs.SetInt("PiggyCount", 1);
                PlayerPrefs.Save();
                ResourceBoost.Instance.piggy = 1;
                buttonPiggyText.text = "Owned";
            }
            else if (indexValue == 1)
            {
                PlayerPrefs.SetInt("DoggyCount", 1);
                PlayerPrefs.Save();
                ResourceBoost.Instance.doggy = 1;
                buttonDoggyText.text = "Owned";
            }
            else if (indexValue == 2)
            {
                PlayerPrefs.SetInt("GoatyCount", 1);
                PlayerPrefs.Save();
                ResourceBoost.Instance.goaty = 1;
                buttonGoatyText.text = "Owned";
            }
            else if (indexValue == 3)
            {
                PlayerPrefs.SetInt("Turtle", 1);
                PlayerPrefs.Save();
                ResourceBoost.Instance.turtle = 1;
                buttonTurtleText.text = "Owned";
            }
            else if (indexValue == 4)
            {
                PlayerPrefs.SetInt("Koala", 1);
                PlayerPrefs.Save();
                ResourceBoost.Instance.koala = 1;
                buttonKoalaText.text = "Owned";
            }
            else if (indexValue == 5)
            {
                PlayerPrefs.SetInt("Sheep", 1);
                PlayerPrefs.Save();
                ResourceBoost.Instance.sheep = 1;
                buttonSheepText.text = "Owned";
            }
            else if (indexValue == 6)
            {
                PlayerPrefs.SetInt("Duck", 1);
                PlayerPrefs.Save();
                ResourceBoost.Instance.duck = 1;
                buttonDuckText.text = "Owned";
            }
            return;
        }

        Double _ownedSolAmount = await Web3.Instance.WalletBase.GetBalance();

        if (_ownedSolAmount <= 0)
        {
            buyingStatusText.text = "Not Enough SOL";
            return;
        }

        HideButtons();

        float costValue = 0.002f;
        buyingStatusText.text = "Buying NFT...";
        buyingStatusText.gameObject.SetActive(true);
        try
        {
            RequestResult<string> result = await Web3.Instance.WalletBase.Transfer(
               new PublicKey("Hw1VoYsnB7kX5h4nZiczEndj6mMF3i7DZR5Q2Ng1JiM4"),
               Convert.ToUInt64(costValue * SolLamports));
            HandleResponse(result);

            buyingStatusText.text = "Minting NFT...";
            buyingStatusText.gameObject.SetActive(true);

            // Prepare the transaction
            var blockHash = await Web3.Rpc.GetLatestBlockHashAsync();
            var minimumRent = await Web3.Rpc.GetMinimumBalanceForRentExemptionAsync(TokenProgram.MintAccountDataSize);
            var transaction = new TransactionBuilder()
                .SetRecentBlockHash(blockHash.Result.Value.Blockhash)
                .SetFeePayer(Web3.Account)
                .AddInstruction(
                    SystemProgram.CreateAccount(
                        Web3.Account,
                        mint.PublicKey,
                        minimumRent.Result,
                        TokenProgram.MintAccountDataSize,
                        TokenProgram.ProgramIdKey))
                .AddInstruction(
                    TokenProgram.InitializeMint(
                        mint.PublicKey,
                        0,
                        Web3.Account,
                        Web3.Account))
                .AddInstruction(
                    AssociatedTokenAccountProgram.CreateAssociatedTokenAccount(
                        Web3.Account,
                        Web3.Account,
                        mint.PublicKey))
                .AddInstruction(
                    TokenProgram.MintTo(
                        mint.PublicKey,
                        associatedTokenAccount,
                        1,
                        Web3.Account))
                .AddInstruction(MetadataProgram.CreateMetadataAccount(
                    PDALookup.FindMetadataPDA(mint),
                    mint.PublicKey,
                    Web3.Account,
                    Web3.Account,
                    Web3.Account.PublicKey,
                    metadata,
                    TokenStandard.NonFungible,
                    true,
                    true,
                    null,
                    metadataVersion: MetadataVersion.V3))
                .AddInstruction(MetadataProgram.CreateMasterEdition(
                        maxSupply: null,
                        masterEditionKey: PDALookup.FindMasterEditionPDA(mint),
                        mintKey: mint,
                        updateAuthorityKey: Web3.Account,
                        mintAuthority: Web3.Account,
                        payer: Web3.Account,
                        metadataKey: PDALookup.FindMetadataPDA(mint),
                        version: CreateMasterEditionVersion.V3
                    )
                );
            var tx = Transaction.Deserialize(transaction.Build(new List<Account> { Web3.Account, mint }));

            // Sign and Send the transaction
            try
            {
                var res = await Web3.Wallet.SignAndSendTransaction(tx);
                // Show Confirmation
                if (res?.Result != null)
                {
                    await Web3.Rpc.ConfirmTransaction(res.Result, Commitment.Confirmed);
                    Debug.Log("Minting succeeded, see transaction at https://explorer.solana.com/tx/"
                              + res.Result + "?cluster=" + Web3.Wallet.RpcCluster.ToString().ToLower());
                }


                buyingStatusText.text = "NFT Minted!";
                buyingStatusText.gameObject.SetActive(true);

                ShowButtons();

                // Hide the NFT Buttons.
                if (indexValue == 0)
                {
                    PlayerPrefs.SetInt("PiggyCount", 1);
                    PlayerPrefs.Save();
                    ResourceBoost.Instance.piggy = 1;
                    buttonPiggyText.text = "Owned";
                }
                else if (indexValue == 1)
                {
                    PlayerPrefs.SetInt("DoggyCount", 1);
                    PlayerPrefs.Save();
                    ResourceBoost.Instance.doggy = 1;
                    buttonDoggyText.text = "Owned";
                }
                else if (indexValue == 2)
                {
                    PlayerPrefs.SetInt("GoatyCount", 1);
                    PlayerPrefs.Save();
                    ResourceBoost.Instance.goaty = 1;
                    buttonGoatyText.text = "Owned";
                }
                else if (indexValue == 3)
                {
                    PlayerPrefs.SetInt("Turtle", 1);
                    PlayerPrefs.Save();
                    ResourceBoost.Instance.turtle = 1;
                    buttonTurtleText.text = "Owned";
                }
                else if (indexValue == 4)
                {
                    PlayerPrefs.SetInt("Koala", 1);
                    PlayerPrefs.Save();
                    ResourceBoost.Instance.koala = 1;
                    buttonKoalaText.text = "Owned";
                }
                else if (indexValue == 5)
                {
                    PlayerPrefs.SetInt("Sheep", 1);
                    PlayerPrefs.Save();
                    ResourceBoost.Instance.sheep = 1;
                    buttonSheepText.text = "Owned";
                }
                else if (indexValue == 6)
                {
                    PlayerPrefs.SetInt("Duck", 1);
                    PlayerPrefs.Save();
                    ResourceBoost.Instance.duck = 1;
                    buttonDuckText.text = "Owned";
                }

            }
            catch (Exception ex)
            {
                buyingStatusText.text = $"Failed to buy NFT: {ex.Message}";
                buyingStatusText.gameObject.SetActive(true);
                Debug.LogError("Error while claiming NFT: " + ex);

                ShowButtons();

                // Not Hide NFT Buttons

            }
        }
        catch (Exception ex)
        {
            buyingStatusText.text = $"Failed to buy NFT: {ex.Message}";
            buyingStatusText.gameObject.SetActive(true);
            Debug.LogError("Error while claiming NFT: " + ex);

            ShowButtons();
        }
    }
}
