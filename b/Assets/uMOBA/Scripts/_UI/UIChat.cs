using UnityEngine;
using UnityEngine.UI;

public class UIChat : MonoBehaviour {
    public InputField messageInput;
    [SerializeField] Button sendButton;
    [SerializeField] Transform content;
    [SerializeField] Scrollbar scrollbar;
    [SerializeField] ScrollRect scrollRect;
    [SerializeField] GameObject textPrefab;
    [SerializeField] KeyCode[] activationKeys = {KeyCode.Return, KeyCode.KeypadEnter};

    [SerializeField] int keepHistory = 100; // only keep 'n' messages

    void Start() {
        // scrolling makes content visible
        scrollbar.onValueChanged.AddListener((val) => {
            ReshowMessages();
        });
    }

    void Update() {
        var player = Utils.ClientLocalPlayer();
        if (!player) return;

        // character limit
        var chat = player.GetComponent<Chat>();
        messageInput.characterLimit = chat.maxLength;

        // activation        
        if (Utils.AnyKeyUp(activationKeys)) messageInput.Select();

        // end edit listener
        messageInput.onEndEdit.SetListener((value) => {
            // submit key pressed?
            if (Utils.AnyKeyDown(activationKeys)) {
                // submit
                string newinput = chat.OnSubmit(value);

                // set new input text
                messageInput.text = newinput;
                messageInput.MoveTextEnd(false);
            }

            // unfocus the whole chat in any case. otherwise we would scroll or
            // activate the chat window when doing wsad movement afterwards
            UIUtils.DeselectCarefully();
        });

        // send button
        sendButton.onClick.SetListener(() => {
            // submit
            string newinput = chat.OnSubmit(messageInput.text);

            // set new input text
            messageInput.text = newinput;
            messageInput.MoveTextEnd(false);

            // unfocus the whole chat in any case. otherwise we would scroll or
            // activate the chat window when doing wsad movement afterwards
            UIUtils.DeselectCarefully();
        });
    }

    void AutoScroll() {
        // update first so we don't ignore recently added messages, then scroll
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0;
    }


    public void AddMessage(MessageInfo msg) {
        // delete an old message if we have too many
        if (content.childCount >= keepHistory)
            Destroy(content.GetChild(0).gameObject);

        // reshow all previous messages (without the new one yet, it will be
        // shown by default)
        ReshowMessages();

        // instantiate and initialize text prefab
        var go = (GameObject)Instantiate(textPrefab);
        go.transform.SetParent(content.transform, false);
        go.GetComponent<Text>().text = msg.content;
        go.GetComponent<Text>().color = msg.color;

        AutoScroll();
    }

    void ReshowMessages() {
        foreach (var fa in GetComponentsInChildren<UIFadeAlpha>())
            fa.ShowAndFade();
    }
}
