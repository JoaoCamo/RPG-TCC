from flask import Flask
from app.routes.main_story_route import main_story_blueprint
from app.routes.story_route import story_blueprint
from app.routes.dungeon_route import dungeon_blueprint
from app.routes.character_route import character_blueprint


def create_app():
    app = Flask(__name__)

    app.register_blueprint(main_story_blueprint, url_prefix="/generate/main_story")
    app.register_blueprint(story_blueprint, url_prefix="/generate/story")
    app.register_blueprint(dungeon_blueprint, url_prefix="/generate/dungeon")
    app.register_blueprint(character_blueprint, url_prefix="/generate/character")

    return app
