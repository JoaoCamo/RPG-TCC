from flask import Blueprint, request, jsonify
from services.main_story_service import generate_main_story

main_story_blueprint = Blueprint("story", __name__)


@main_story_blueprint.route("/", methods=["POST"])
def main_story():
    return
