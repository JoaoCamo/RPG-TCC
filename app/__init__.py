from flask import Flask

def create_app():
    app = Flask(__name__)

    # Register route blueprints
    from .routes.main_story import main_story
    from .routes.story import story
    from .routes.dungeon import dungeon
    from .routes.character import character

    app.register_blueprint(main_story.bp, url_prefix="api/main_story")
    app.register_blueprint(story.bp, url_prefix="/api/story")
    app.register_blueprint(dungeon.bp, url_prefix="/api/dungeon")
    app.register_blueprint(character.bp, url_prefix="/api/character")

    return app