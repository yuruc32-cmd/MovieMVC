# 反思心得
在這次的MVC作業中，我完成了一個**電影預約系統**，涵蓋了使用者註冊與登入、電影預約、新增/編輯/刪除預約、查詢功能，以及詳細資料呈現等功能。這個作業對我來說是一個很好的實務練習，也讓我在學習ASP.NETCoreMVC的過程中，有了更深入的體驗與反思。

首先，在**使用者帳號管理部分**，我設計了登入與註冊頁面，並提供了測試帳號資訊，讓系統在開發或測試階段方便操作。透過這個過程，我更理解了Model、View、Controller的互動，例如在登入時透過Controller驗證資料，並將訊息傳回View以顯示錯誤或成功通知。這也讓我體會到使用TempData與ViewBag的差異，以及它們在不同頁面之間傳遞訊息的應用場景。

其次，在**電影預約功能**上，我學到如何將資料表操作與MVC結合。使用者可以新增、修改、刪除自己的電影預約，並能查看詳細資料。這個過程中，我遇到了一些挑戰，例如如何在Edit頁面中保護使用者資訊（密碼與姓名設為唯讀），以及如何在刪除時顯示確認訊息，避免誤操作。這讓我認識到**前端與後端互動**的重要性，以及UI設計對使用者體驗的影響。

再者，下拉式查詢功能讓我實作了從Controller將資料傳給View，再透過表單提交查詢的流程。這個功能雖然看似簡單，但讓我更熟悉了LINQ與MVC的資料傳遞方式，也理解了如何在前端使用select控制項動態顯示資料。

在整個開發過程中，我也反思到自己的不足之處，例如目前**系統的安全性尚可提升**，密碼是以明文方式存取，未使用加密或Hash處理；此外，系統對時間場次的輸入限制較少，缺乏完整驗證，也可能導致使用者輸入不合理資料。這提醒我在未來的專案中，需要更注重**資料驗證與安全設計**。

最後，這個作業也讓我體會到**前端設計與使用者體驗**的重要性。從登入頁面的排版、卡片設計，到預約清單表格的呈現，再到刪除操作的確認提示，每一個小細節都會影響使用者的感受。我學會了如何使用Bootstrap快速建立美觀且直覺的介面，也理解了CSS與Razor結合的技巧。

總結來說，這次作業讓我從理論走向實作，全面體驗了MVC架構下的專案流程，從資料操作、控制流程到前端呈現都能親手完成。未來我希望可以加強資料驗證、安全性設計，以及前端互動體驗，讓系統更完整、穩健，也更加貼近真實商業應用場景。

執行畫面   影片：https://youtu.be/xV3WSs9OMvw?si=VYdGs0YB-N66G1yR
<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/d0cb778f-c73e-473d-a5a5-b62156f4b6c5" />

<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/1596cd9f-aa61-4746-9467-f040d18ff5e4" />

<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/960d005d-401c-43bd-84da-17302ca4c420" />

<img width="1915" height="523" alt="image" src="https://github.com/user-attachments/assets/ae1ecd9b-33e6-4702-a3b4-daf6ff5149d3" />

<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/ac9e06d7-f86d-4970-8367-528a18cb4943" />


<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/acc179a3-6522-427d-bfdd-49053dd12932" />



