# WORKFLOW.md

Як працювати з SoundCloud Clone: `main` + `develop` + `feature/*`.

## Структура гілок

```
main
 └── develop
      ├── feature/auth
      ├── feature/music-crud
      ├── feature/playlist-crud
      ├── feature/comment-crud
      ├── feature/audio-streaming
      └── ...
```

### `main`
- Завжди стабільна, робоча версія готова до презентації.
- Приймає merge **тільки з `develop`**.
- Прямі коміти в `main` заборонені.

### `develop`
- Робоча інтеграційна гілка. Сюди мерджаться всі `feature/*` гілки через Pull Request.
- Перед merge у `main` має бути в робочому стані.

### `feature/*`
- Одна гілка на одну задачу/фічу.
- Створюється **від `develop`**, мерджиться **назад у `develop`**.
- Іменування: `feature/<коротка-назва>`, напр. `feature/music-crud`, `feature/playlist-track-order`.
- Мерджимо якнайшвидше після готовності.


### Як назвати гілку:
```
feature/auth-jwt
feature/video-upload
fix/login-redirect
```
## Коміти

Пиши зрозумілі коміт-меседжі:

```
✅ Add JWT token generation
✅ Fix login form validation
✅ Remove unused imports

❌ fix
❌ asdfgh
❌ changes
❌ test123
```

## Типовий цикл роботи

```bash
# 1. Оновити develop
git checkout develop
git pull origin develop

# 2. Створити feature-гілку
git checkout -b feature/comment-crud

# 3. Робота + коміти
git add .
git commit -m "feat: add Comment entity and DbSet"

# 4. Перед Pull Request — підтягнути свіжий develop у свою гілку
git checkout develop
git pull origin develop
git checkout feature/comment-crud
git merge develop

# 5. Запушити і відкрити PR: feature/comment-crud → develop
git push origin feature/comment-crud
```

## Заборонено

- ❌ Пушити напряму в `main`
- ❌ Мерджити без PR
- ❌ Ігнорувати конфлікти і пушити через force
- ❌ Коміти типу `fix`, `test`, `aaa`


## Цикл роботи для merge `develop` у `main`
```bash
# 1. Оновити обидві гілки
git checkout develop
git pull origin develop

git checkout main
git pull origin main

# 2. Змерджити develop у main з явною merge-точкою
git merge --no-ff develop -m "Merge develop into main: checkpoint 02.09"

# 3. Запушити main
git push origin main

```