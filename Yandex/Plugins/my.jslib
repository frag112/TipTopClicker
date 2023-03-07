mergeInto(LibraryManager.library, {
  GiveMePlayerData: function () {
    myGameInstance.SendMessage('Yandex', 'SetName', player.getName())  // gameobject name, method, value to pass
    myGameInstance.SendMessage('Yandex', 'SetPhoto', player.getPhoto('medium'))
  },
  RateGame: function () {
    ysdk.feedback.canReview().then(({ value, reason }) => {
      if (value) {
        ysdk.feedback.requestReview().then(({ feedbackSent }) => {
          console.log(feedbackSent)
          // myGameInstance.SendMessage() do not show this again on this user
        })
      } else {
        console.log(reason)
      }
    })
  },
  SavePlayerData: function (data) {
    var dataString = UTF8ToString(data);
    var playerData = JSON.parse(dataString);
    player.setData(playerData, true);
  },
  LoadPlayerData: function () {
    player.getData().then(data =>{
      const playerData = JSON.stringify(data);
      myGameInstance.SendMessage('PlayerDataHolder', 'LoadData', playerData);
    });
  },
})
