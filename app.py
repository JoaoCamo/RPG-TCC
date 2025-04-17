from flask import Flask
from routes.main_story import main_story_blueprint
from routes.story import story_blueprint
from routes.dungeon import dungeon_blueprint
from routes.character import character_blueprint

app = Flask(__name__)

# Register Blueprints
app.register_blueprint(main_story_blueprint, url_prefix='/generate/story')
app.register_blueprint(story_blueprint, url_prefix='/generate/story')
app.register_blueprint(dungeon_blueprint, url_prefix='/generate/dungeon')
app.register_blueprint(character_blueprint, url_prefix='/generate/character')

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000)