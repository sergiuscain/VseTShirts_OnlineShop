<h1>Онлайн магазин одежды</h1>

(В процессе написания и разработки!)

<h1>Используемые технологии:</h1>
<ul>
  <li>
    <ul>
       <li><h4>Базы данных:</h4></li>
      <li>Entity Framework</li>
      <li>MsSql</li>
      <li>PostgreSql (через EF на неё переключался, потом вернул обратно на MsSQL)</li>
      <li>LINQ</li>
      <li>Миграции</li>
    </ul>
  </li>
  <li>
    <ul>
      <li>Web:</li>
      <li>ASP.NET</li>
      <li>MVC</li>
      <li>Частичные представления и компоненты представлений</li>
      <li>HTML</li>
      <li>Bootstrap</li>
    </ul>
  </li>
  <li>
    <ul>
      <li>Прочее:</li>
      <li>Работа с ООП</li>
      <li>Dependency Injection</li>
      <li>Области area</li>
      <li>Работа с файловой системой</li>
      <li>Маппинг и собственный автомаппер через рефлексию</li>
      <li>C# и .Net</li>
    </ul>
  </li>
</ul>
<h1>Функционал сайта:</h1>
<h3>1) Главная страница: на ней находится весь список товаров. </h5><br/>
<img src="https://github.com/user-attachments/assets/2cf88a1f-0ed9-4ef4-9a91-2dc06146f27c" />
<h4>На главной странице есть фильтры и поиск. Поиск идёт по названию товара</h2>
<img src="https://github.com/user-attachments/assets/7a2aee20-d3c3-4e69-bd8f-c112ba1e9263" />
<h4>Список фильтров сделан в виде выпадающего меню. Можно фильтровать товары по диапазону цены и количеству на складе,
<h4>А также по параметрам товара: Категория, Цвет, Размер и Пол.</h4>
<img src="https://github.com/user-attachments/assets/76f7fb35-d8fa-47a4-9d6f-9d1433a3e3ac" />
<h4>Также на главной странице есть список коллекций (товары, объединенные по одной тематике. В моем случае, просто товары, которые рандомно добавлены в стандартные коллекции.<br /> Управление коллекциями находится в меню администратора)</h4>
<div>
  <img width="500" src="https://github.com/user-attachments/assets/4c607739-124d-4c76-bc33-16c1fe53790e" />
  <img width="500" src="https://github.com/user-attachments/assets/e93b729a-8396-4a93-a3d8-d1d18e53f7d6"
</div>
<h4>Товары можно сравнить между собой.</h4>
<img src="https://github.com/user-attachments/assets/f5f6e18a-17f4-4a00-8f3a-f9dd2bf5968c">
<h4>Так-же, на главной странице и на странице сравнения товаров, можно перейти в карточку товара. В ней есть баннер с изображениями товаров, описания и возможность добавить в корзину</h4>
  <img src="https://github.com/user-attachments/assets/5ee3143e-a3ec-4989-9d81-dac01df7bb7c">
  <img src="https://github.com/user-attachments/assets/58190c67-4800-4352-91e3-e17f833e728a">
  <img src="https://github.com/user-attachments/assets/1a818d9a-89b1-422d-a257-d5a47539672f">
  <h3>Избранное</h3>
  <h4>Во складке избранное отображаются товары авторизованного пользователя, которые он добавил туда.</h4>
  <h4>Есть возможность удалить из избранного, перейти в карточку товара(по названию кликнуть), сравнить и добавить в корзину</h4>
  <h6>(так-же, в хэдере динамически обновляется счётчик товаров в избранном)</h6>
  <img src="https://github.com/user-attachments/assets/e3cf4d39-00b4-4535-94fc-3273785211a1">
<h1>Профиль пользователя</h1>
<h4>Нажав на ник пользователя (у админа Никнейм такой-же, как и Email просто), Мы переходим в личный кабинет(пока он не доделан немного)</h4>
    <img src="https://github.com/user-attachments/assets/ef037a3b-bb57-47d9-a05b-4a38d394ab98">
<h1>Авторизация и регистрация</h1>
<h4>Выполнена с помощью ASP.NET Core Identity</h4>
  <img src="https://github.com/user-attachments/assets/fd6b80cf-b686-4b5f-b650-06ecb10e35ac">

<h4></h4>
