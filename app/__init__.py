from flask import Flask
from app.routes.main_story_route import main_story_blueprint
from app.routes.story_route import story_blueprint
from app.routes.dungeon_room_route import dungeon_room_blueprint
from app.routes.dungeon_layout_route import dungeon_layout_blueprint
from app.routes.character_route import character_blueprint
from app.routes.item_route import item_blueprint


def create_app():
    app = Flask(__name__)

    app.register_blueprint(main_story_blueprint, url_prefix="/generate/main_story")
    app.register_blueprint(story_blueprint, url_prefix="/generate/story")
    app.register_blueprint(dungeon_room_blueprint, url_prefix="/generate/dungeon_room")
    app.register_blueprint(dungeon_layout_blueprint, url_prefix="/generate/dungeon_layout")
    app.register_blueprint(character_blueprint, url_prefix="/generate/character")
    app.register_blueprint(item_blueprint, url_prefix="/generate/item")

    return app
